using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Common.Constants;
using DataAccess.Entities;
using Utils.Helpers;
using DataAccess.Enums;
using Auth.JWT;
using Auth.Claims;
using Model;
using Common.Exceptions;
using DataAccess.Repositories;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IAuth
    {
        User? CurrentUser { get; }
        Compania? CurrentCompany { get; }
        //DataSourceEnum DataSource { get; }

        //IUserApiKeyPermission UserApiKeyPermissions { get; }
        void SetCurrentCredentials(string? authToken, string? location, IPAddress? ipAddress = null, string? userAgent = null);
        JwtResponse Login(Login login, string? host = null);
        bool Register(RegisterDto registerDto);
        //JwtResponse RefreshToken(string? host = null);
        User FindUserByToken(string? authToken, string? location, IPAddress? ipAddress = null);
        string? FindCompanyRNCByToken(string? authToken, string? location);
        void UpdateUserRoles(string? userId, List<Role> roles);
        //void UpdateUserPassword(PasswordChangeDto passwordChange);
        bool IsValidToken(User user, string? token, out JwtSecurityToken? jwt);
        //bool ResetPassword(PasswordChangeDto login, string? recoveryToken);
        bool RevokeToken(string? token);
    }

    public class AuthRepo : IAuth
    {
        public User? CurrentUser { get; private set; }

        public Compania? CurrentCompany { get; private set; }

        // public DataSourceEnum DataSource { get; private set; } = DataSourceEnum.Unknown;

        public IUser Users { get; }

        public IUserToken UserTokens { get; }

        public IRole Roles { get; }

        public IUserCompany UserCompanies { get; }

        public IPermission Permissions { get; }

        //public IUserApiKey UserApiKeys { get; }

        public IRolePermission RolePermissions { get; }

        public IUserPermission UserPermissions { get; }

        //public IUserApiKeyPermission UserApiKeyPermissions { get; }

        private readonly JwtConfig jwtConfig;

        private readonly PGIContext dBContext;

        public AuthRepo(PGIContext dBContext,
                        IUser user,
                        IUserToken userToken,
                        IUserCompany userCompany,
                        IRole role,
                        IPermission permissions,
                        // IUserApiKey userApiKeys,
                        IRolePermission rolePermissions,
                        IUserPermission userPermissions,
                        // IUserApiKeyPermission userApiKeyPermissions,
                        IOptionsMonitor<JwtConfig> options)
        {

            this.dBContext = dBContext;

            Users = user;
            UserTokens = userToken;
            UserCompanies = userCompany;
            Roles = role;
            Permissions = permissions;
            //UserApiKeys = userApiKeys;
            RolePermissions = rolePermissions;
            UserPermissions = userPermissions;
            //UserApiKeyPermissions = userApiKeyPermissions;

            jwtConfig = options.CurrentValue;
        }

        public void SetCurrentCredentials(string? authToken, string? location, IPAddress? ipAddress = null, string? userAgent = null)
        {
            var authTask = FindUserByToken(authToken, location, ipAddress);

            CurrentUser = authTask;

            var companyRNC = FindCompanyRNCByToken(authToken, location);

            CurrentCompany = dBContext.Companias.SingleOrDefault(x => x.Rnc == companyRNC);

            //TrySetDataSource(location, userAgent);
        }

        public User FindUserByToken(string? authToken, string? location, IPAddress? ipAddress = null)
        {
            var token = GetJwtTokenFromAuthHeader(authToken, location, out string? tokenType);

            var tokenHandler = new JwtSecurityTokenHandler();

            var userToken = tokenHandler.ReadJwtToken(token);

            var userName = userToken?.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName)?.Value;
            var companyTaxId = userToken?.Claims.SingleOrDefault(x => x.Type == CustomClaimTypes.Company)?.Value;

            var user = Users.FindValidByUsername(userName)
                ?? throw new CustomException("User not founded");

            if (tokenType.Equals(AppConstants.BEARER_TOKEN, StringComparison.InvariantCultureIgnoreCase)
                && UserTokens.Find(x =>
                        x.AccessToken == token
                        && x.UserId == user.Id
                        && x.Exp > DateTime.UtcNow) == null
                )
                throw new CustomException("Invalid token");

            if (!IsValidToken(user, token, out JwtSecurityToken? jwt))
                throw new CustomException("Invalid token");

            return (user);

        }

        public string? FindCompanyRNCByToken(string? authToken, string? location)
        {
            var token = GetJwtTokenFromAuthHeader(authToken, location, out string? tokenType);

            var tokenHandler = new JwtSecurityTokenHandler();

            var userToken = tokenHandler.ReadJwtToken(token);

            return (userToken?.Claims.SingleOrDefault(x => x.Type == CustomClaimTypes.Company)?.Value);
        }

        private static string? GetJwtTokenFromAuthHeader(string? authToken, string? location, out string? tokenType)
        {
            tokenType = string.Empty;

            if (string.IsNullOrWhiteSpace(authToken))
                throw new LoginException("Token invalido");

            if (location == AppConstants.API_KEY_TOKEN)
            {
                tokenType = AppConstants.API_KEY_TOKEN;
                return authToken;
            }

            var tokenFragments = authToken.Split(" ", 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (tokenFragments.Length != 2)
                throw new Exception("Token not valid");

            tokenType = tokenFragments[0];

            if (!tokenType.Contains(AppConstants.BEARER_TOKEN, StringComparison.InvariantCultureIgnoreCase))
                throw new Exception("Invalid token type");

            return tokenFragments[1];
        }

        public bool IsValidToken(User user, string? token, out JwtSecurityToken? jwt)
        {
            jwt = null;

            try
            {
                var tokenValidator = new JwtSecurityTokenHandler();
                var parms = jwtConfig.Parameters;
                parms.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(user.PasswordHash));

                tokenValidator.ValidateToken(token, parms, out SecurityToken securityToken);

                jwt = securityToken as JwtSecurityToken;
                return true;
            }
            catch (Exception ex)
            {
                new LogData().Error(ex);
            }

            return false;

        }

        public JwtResponse Login(Login login, string? host = null)
        {
            User? user = null;

            try
            {
                if (login == null) throw new LoginException();

                user = Users.FindByUsername(login.username)
                    ?? throw new LoginException("Invalid username or password");

                //if (user.LockoutDueDate is not null && DateTime.Now > user.LockoutDueDate)
                //    ResetLockout(user);

                //if ((user.Active == 0 || user.LockoutEnabled == 0 /*|| user.IsLocked()*/) && user.LockoutDueDate < DateTime.Now)
                //    throw new LoginException("Inactive User or Locked") { ErrorCode = "1001" };
                if(!user.Active)
                if (!IsValidPassword(login.password, user))
                    throw new LoginException("Invalid username or password");

                var company = dBContext.GetService<ICompania>()?.Find(x => x.TaxIdNumber == login.CompanyRNC);

                if (!IsValidUserCompany(user, login.CompanyRNC, company))
                    throw new LoginException("Invalid company") { ErrorCode = "1002" };

                var token = GenerateUserToken(user, host, login.CompanyRNC, out JwtSecurityToken? securityToken);

                UserTokens.SaveToken(user, securityToken!, token, host);

                return new JwtResponse()
                {
                    Token = token,
                    Expiration = securityToken!.ValidTo.ToLocalTime()
                };
            }
            catch (LoginException ex)
            {
                if (user is not null && ex.ErrorCode != "1001")
                    IncreateLoginTries(user);

                throw;
            }
        }

        public Task<JwtResponse> RefreshToken(string? host = null)
        {
            if (CurrentUser is null)
                throw new LoginException();

            var token = GenerateUserToken(CurrentUser, host, CurrentCompany?.Rnc, out JwtSecurityToken? securityToken);

            UserTokens.SaveToken(CurrentUser, securityToken!, token, host);

            return Task.FromResult(new JwtResponse()
            {
                Token = token,
                Expiration = securityToken!.ValidTo.ToLocalTime()
            });

        }

        private bool IsValidUserCompany(User user, string? companyRNC, Compania? company)
        {
            if (string.IsNullOrWhiteSpace(companyRNC))
                return false;

            if (company is null)
                return false;

            if (user.Su)
                return true;

            if (!company.Active)
                return false;

            //var isCurrentCompany = user.Company?.TaxIdNumber == companyRNC;
            var hasCompany = UserCompanies.Find(x => x.CompaniaId == company.Id && x.UserId == user.Id) != null;

            return hasCompany;
        }

        //private void ResetLockout(User user)
        //{
        //    try
        //    {
        //        //user.ResetLockout();

        //        //Users.UpdateSaving(user);
        //    }
        //    catch { }
        //}

        private void IncreateLoginTries(User user)
        {
            try
            {
                user.AccessFailedCount++;

                if (user.AccessFailedCount >= 3)
                {
                    user.LockoutEnabled = true;
                    user.LockoutDueDate = DateTime.Now.AddHours(12);
                }

                Users.UpdateSaving(user);
            }
            catch { }
        }

        private string? GenerateUserToken(User user, string? host, string? companyRNC, out JwtSecurityToken? securityToken)
        {
            // INIT TOKEN HANDLER
            var tokenHandler = new JwtSecurityTokenHandler();

#warning workaround, cambiar luego

            //securityToken = new UserTokenBuilder(user)
            //   .SetIssuer(jwtConfig.Issuer)
            //   .SetHost(host)
            //   .SetCompany(companyRNC)
            //   .Build();
            securityToken = null;
            return "";
            //return tokenHandler.WriteToken(securityToken);
        }

        public bool Register(RegisterDto registerDto)
        {
            Users.AddSaving(new User()
            {
                Username = registerDto.UserName,
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = Utilities.Hash(registerDto.Password, HashAlg.SHA256)
            });

            return (true);
        }

        private static bool IsValidPassword(string? passToValidate, User user)
        {
            if (string.IsNullOrWhiteSpace(passToValidate)) return false;
            if (user == null) return false;
            if (string.IsNullOrWhiteSpace(user?.PasswordHash)) return false;

            var passwordHashed = Utilities.Hash(passToValidate, HashAlg.SHA256) ?? "";

            return passwordHashed.Equals(user.PasswordHash);

        }

        public void UpdateUserRoles(string? userId, List<Role> roles)
        {
            // TODO: update user roles
#warning workaround, cambiar luego
            //throw new NotImplementedException();
        }

//        public void UpdateUserPassword(PasswordChangeDto passwordChange)
//        {
//            // TODO: implement update password feature
//#warning workaround, cambiar luego
//            throw new NotImplementedException();
//        }

        //public bool ResetPassword(PasswordChangeDto login, string? recoveryToken)
        //{
        //    // TODO: implement reset password feature
        //    throw new NotImplementedException();
        //}

        public bool RevokeToken(string? userToken)
        {
#warning workaround, cambiar luego
            //var token = TokenRepo.Find(x => x.Token == userToken);
            //token.IsRevoked = true;
            //TokenRepo.UpdateSaving(token);
            return true;
        }

    }
}
