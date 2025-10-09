using Auth.Claims;
using Auth.JWT;
using Common.Constants;
using Common.Exceptions;
using DataAccess.Builders;
using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Windows.Input;
using Utils.Extensions;
using Utils.Helpers;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IAuth
    {
        string? Session { get; }
        User? CurrentUser { get; }
        Compania? CurrentCompany { get; }
        DireccionIntitucional? CurrentDireccionIntitucional { get; }
        Sucursal? CurrentSucursal { get; }
        DataSourceEnum DataSource { get; }

        IUser Users { get; }
        IRole Roles { get; }
        IUserToken UserTokens { get; }
        IUserCompany UserCompanies { get; }
        IPermission Permissions { get; }
        //IUserApiKey UserApiKeys { get; }
        IRolePermission RolePermissions { get; }
        IUserPermission UserPermissions { get; }
        //IUserApiKeyPermission UserApiKeyPermissions { get; }
        void SetCurrentCredentials(string authToken, string location, IPAddress? ipAddress = null, string? userAgent = null);
        Task<JwtResponse> Login(Login login, string? host = null);
        Task<bool> Register(RegisterDto registerDto);
        Task<JwtResponse> RefreshToken(string? host = null);
        Task<User> FindUserByToken(string authToken, string location, IPAddress? ipAddress = null);
        Task<string?> FindCompanyRNCByToken(string authToken, string location);
        void UpdateUserRoles(string userId, List<Role> roles);
        void UpdateUserPassword(PasswordChangeDto passwordChange);
        bool IsValidToken(User user, string token, out JwtSecurityToken? jwt);
        bool ResetPassword(PasswordChangeDto login, string recoveryToken);
        bool RevokeToken(string token);
        JwtResponse SelectCompania(string id);
        JwtResponse SelectDireccionInstitucional(string id);
        JwtResponse SelectSucursal(string id);
    }

    public class AuthRepo : IAuth
    {
        public string? Session { get; private set; }

        public User? CurrentUser { get; private set; }

        public Compania? CurrentCompany { get; private set; }

        public DireccionIntitucional? CurrentDireccionIntitucional { get; private set; }

        public Sucursal? CurrentSucursal { get; private set; }

        public DataSourceEnum DataSource { get; private set; } = DataSourceEnum.Unknown;

        public IUser Users { get; }

        public IUserToken UserTokens { get; }

        public IRole Roles { get; }

        public IUserCompany UserCompanies { get; }

        public IPermission Permissions { get; }

        //public IUserApiKey UserApiKeys { get; }

        public IRolePermission RolePermissions { get; }

        public IUserPermission UserPermissions { get; }


        IUserDireccionInstitucional userDireccionInstitucionalRepo { get; }
        IUserSucursal userSucursalRepo { get; }
        //public IUserApiKeyPermission UserApiKeyPermissions { get; }

        private readonly JwtConfig jwtConfig;

        private readonly PGIContext dBContext;

        public AuthRepo(PGIContext dBContext,
                        IUser user,
                        IUserToken userToken,
                        IUserCompany userCompany,
                        IRole role,
                        IPermission permissions,
                        //IUserApiKey userApiKeys,
                        IRolePermission rolePermissions,
                        IUserPermission userPermissions,
                        IUserDireccionInstitucional userDireccionInstitucionalRepo,
                        IUserSucursal userSucursalRepo,
                        //IUserApiKeyPermission userApiKeyPermissions,
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

            this.userDireccionInstitucionalRepo = userDireccionInstitucionalRepo;
            this.userSucursalRepo = userSucursalRepo;

            jwtConfig = options.CurrentValue;
        }

        public void SetCurrentCredentials(string authToken, string location, IPAddress? ipAddress = null, string? userAgent = null)
        {
            var authTask = FindUserByToken(authToken, location, ipAddress).GetAwaiter().GetResult();

            //if (!authTask.IsCompletedSuccessfully)
            //{
            //    if (authTask.Exception is not null)
            //        LogData.Error(authTask.Exception);

            //    return;
            //}

            CurrentUser = authTask;


            var token = GetJwtTokenFromAuthHeader(authToken, location, out string tokenType);

            Session = token;

            var tokenHandler = new JwtSecurityTokenHandler();

            var userToken = tokenHandler.ReadJwtToken(token);

            var companyRNC = userToken.Claims.FirstOrDefault(x => x.Type == CustomJwtClaimTypes.Company)?.Value;
            var instDirId = userToken.Claims.FirstOrDefault(x => x.Type == CustomJwtClaimTypes.DireccionInstitucional)?.Value;
            var sucursalId = userToken.Claims.FirstOrDefault(x => x.Type == CustomJwtClaimTypes.BranchOffice)?.Value;

            SetCompany(companyRNC);
            SetDirInst(instDirId);
            SetSucursal(sucursalId);

            //TrySetDataSource(location, userAgent);

            void SetCompany(string? rnc)
            {
                if (string.IsNullOrWhiteSpace(rnc))
                    return;
                try
                {
                    CurrentCompany = string.Equals(CurrentUser?.Company?.Rnc, rnc, StringComparison.InvariantCultureIgnoreCase)
                    ? CurrentUser?.Company
                    : CurrentUser?.Companies.SingleOrDefault(x => x.Rnc == rnc);
                }
                catch { }
            }
            void SetDirInst(string? DirInstId)
            {
                if (string.IsNullOrWhiteSpace(DirInstId))
                    return;
                try
                {
                    CurrentDireccionIntitucional = CurrentDireccionIntitucional?.Id == DirInstId
                    ? CurrentDireccionIntitucional
                    : userDireccionInstitucionalRepo.Find(x => x.Id == DirInstId && x.UserId == CurrentUser.Id)?.DireccionIntitucional;
                }
                catch { }
            }
            void SetSucursal(string? sucursalId)
            {
                if (string.IsNullOrWhiteSpace(sucursalId))
                    return;
                try
                {
                    CurrentSucursal = CurrentSucursal?.Id == sucursalId
                    ? CurrentSucursal
                    : userSucursalRepo.Find(x => x.Id == sucursalId && x.UserId == CurrentUser.Id)?.Sucursal;
                }
                catch { }
            }
        }

        //private void TrySetDataSource(string location, string? userAgent)
        //{
        //    try
        //    {
        //        if (location.StartsWith(AppConstants.API_KEY_TOKEN, StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            DataSource = DataSourceEnum.Integration;
        //            return;
        //        }

        //        var uaInfo = UserAgentHelper.Parse(userAgent);

        //        if (uaInfo is null)
        //            return;

        //        if (uaInfo.IsMobile)
        //        {
        //            DataSource = DataSourceEnum.Mobile;
        //            return;
        //        }

        //        if (uaInfo.IsBrowser)
        //        {
        //            DataSource = DataSourceEnum.WebClient;
        //            return;
        //        }
        //    }
        //    catch { }

        //    DataSource = DataSourceEnum.Unknown;
        //}

        public Task<User> FindUserByToken(string authToken, string location, IPAddress? ipAddress = null)
        {
            try
            {
                var token = GetJwtTokenFromAuthHeader(authToken, location, out string tokenType);

                var tokenHandler = new JwtSecurityTokenHandler();

                var userToken = tokenHandler.ReadJwtToken(token);

                var userName = userToken?.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName)?.Value;
                //var companyTaxId = userToken?.Claims.SingleOrDefault(x => x.Type == CustomJwtClaimTypes.Company)?.Value;

                var user = Users.FindValidByUsername(userName)
                    ?? throw new UnauthorizedException();

                if (tokenType.Equals(AppConstants.BEARER_TOKEN, StringComparison.InvariantCultureIgnoreCase)
                    && !UserTokens.Exists(x =>
                            x.AccessToken == token
                            && x.UserId == user.Id
                            && x.Exp > DateTime.UtcNow)
                    )
                    throw new UnauthorizedException("Invalid token");

                //else if (tokenType.Equals(AppConstants.API_KEY_TOKEN, StringComparison.InvariantCultureIgnoreCase))
                //{
                //    var userApiKey = UserApiKeys.Find(x =>
                //        x.AccessToken == token
                //        && x.UserId == user.Id
                //        && x.Active
                //        && DateTime.UtcNow > x.NotBeforeUTC
                //        && (x.NotAfterUTC == null || DateTime.UtcNow < x.NotAfterUTC.Value)
                //        && x.Company!.TaxIdNumber == companyTaxId
                //    )
                //        ?? throw new UnauthorizedException("Invalid token");

                //    if (!(userApiKey.AllowedIPs is null || userApiKey.AllowedIPs.Any(x => x.Equals("*") || x.Replace("*", "").StartsWith(ipAddress!.ToString(), StringComparison.InvariantCultureIgnoreCase))))
                //        throw new UnauthorizedException("Invalid token");
                //}

                if (!IsValidToken(user, token, out JwtSecurityToken? jwt))
                    throw new UnauthorizedException("Invalid token");

                //if (tokenType.Equals(AppConstants.API_KEY_TOKEN, StringComparison.InvariantCultureIgnoreCase))
                //    user.Permissions = (from t0 in UserApiKeys.EntityDbSet
                //                        join t1 in UserApiKeyPermissions.EntityDbSet on new { t0.Id, t0.Active, t0.UserId, t0.AccessToken } equals new { Id = t1.ApiKeyId, Active = true, UserId = user.Id, AccessToken = token }
                //                        where
                //                             t1.Permission.Active
                //                        select t1.Permission)
                //                        .AsNoTrackingWithIdentityResolution()
                //                        .ToList();

                return Task.FromResult(user);
            }
            catch (Exception ex)
            {
                return Task.FromException<User>(ex);
            }
        }

        public Task<string?> FindCompanyRNCByToken(string authToken, string location)
        {
            try
            {
                var token = GetJwtTokenFromAuthHeader(authToken, location, out string? tokenType);

                var tokenHandler = new JwtSecurityTokenHandler();

                var userToken = tokenHandler.ReadJwtToken(token);

                return Task.FromResult(userToken?.Claims.SingleOrDefault(x => x.Type == CustomJwtClaimTypes.Company)?.Value);
            }
            catch (Exception ex)
            {
                return Task.FromException<string?>(ex);
            }
        }

        private static string GetJwtTokenFromAuthHeader(string authToken, string location, out string tokenType)
        {
            tokenType = string.Empty;

            if (string.IsNullOrWhiteSpace(authToken))
                throw new BadRequestException(nameof(authToken));

            if (location == AppConstants.API_KEY_TOKEN)
            {
                tokenType = AppConstants.API_KEY_TOKEN;
                return authToken;
            }

            var tokenFragments = authToken.Split(" ", 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (tokenFragments.Length != 2)
                throw new BadRequestException("Token not valid");

            tokenType = tokenFragments[0];

            if (!tokenType.In(AppConstants.BEARER_TOKEN))
                throw new BadRequestException("Invalid token type");

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
                LogData.Error(ex);
            }

            return false;

        }

        public Task<JwtResponse> Login(Login login, string? host = null)
        {
            User? user = null;

            try
            {
                if (login == null) throw new UnauthorizedException();

                user = Users.FindByUsername(login.UserName)
                    ?? throw new UnauthorizedException("Invalid username or password");

                if (user.LockoutDueDate is not null && DateTime.Now > user.LockoutDueDate)
                    ResetLockout(user);

                if ((!user.Active || user.IsLocked()) && user.LockoutDueDate < DateTime.Now)
                    throw new UnauthorizedException("Inactive User or Locked") { ErrorCode = 1001 };

                if (!IsValidPassword(login.Password, user))
                    throw new UnauthorizedException("Invalid username or password");

                //var company = dBContext.GetService<ICompania>()?.Find(x => x.Rnc == login.CompanyRNC);

                //if (!IsValidUserCompany(user, login.CompanyRNC, company))
                //    throw new UnauthorizedException("Invalid company") { ErrorCode = 1002 };

                var token = GenerateUserToken(user, host, out JwtSecurityToken? securityToken);

                UserTokens.SaveToken(user, securityToken!, token, host);

                return Task.FromResult(new JwtResponse()
                {
                    Token = token,
                    Expiration = securityToken!.ValidTo.ToLocalTime()
                });
            }
            catch (UnauthorizedException ex)
            {
                if (user is not null && ex.ErrorCode != 1001)
                    IncreateLoginTries(user);

                throw;
            }
        }

        public Task<JwtResponse> RefreshToken(string? host = null)
        {
            if (CurrentUser is null)
                throw new UnauthorizedException();


            var tokenHandler = new JwtSecurityTokenHandler();

            var userToken = tokenHandler.ReadJwtToken(Session);

            var token = GenerateUserToken(CurrentUser, host, [.. userToken.Claims], out JwtSecurityToken? securityToken);

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
                return true;

            if (company is null)
                return false;

            if (user.Su)
                return true;

            if (!company.Active)
                return false;

            var isCurrentCompany = user.Company?.Rnc == companyRNC;
            var hasCompany = UserCompanies.Exists(x => x.CompaniaId == company.Id && x.UserId == user.Id);

            return isCurrentCompany || hasCompany;
        }

        private void ResetLockout(User user)
        {
            try
            {
                user.ResetLockout();

                Users.UpdateSaving(user);
            }
            catch { }
        }

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

        private string GenerateUserToken(User user, string? host, out JwtSecurityToken? securityToken)
        {
            // INIT TOKEN HANDLER
            var tokenHandler = new JwtSecurityTokenHandler();

            securityToken = new UserTokenBuilder(user)
               .SetIssuer(jwtConfig.Issuer)
               .SetHost(host)
               //.SetClaims()
               //.SetCompany(companyRNC)
               .Build();

            return tokenHandler.WriteToken(securityToken);
        }
        private string GenerateUserToken(User user, string? host, Claim[] claims, out JwtSecurityToken? securityToken)
        {
            // INIT TOKEN HANDLER
            var tokenHandler = new JwtSecurityTokenHandler();

            securityToken = new UserTokenBuilder(user, false)
               .SetIssuer(jwtConfig.Issuer)
               .SetHost(host)
               .SetClaims(claims)
               //.SetCompany(companyRNC)
               .Build();

            return tokenHandler.WriteToken(securityToken);
        }

        public Task<bool> Register(RegisterDto registerDto)
        {
            Users.AddSaving(new User()
            {
                Username = registerDto.UserName,
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = CryptoHelper.Hash(registerDto.Password)!
            });

            return Task.FromResult(true);
        }

        private static bool IsValidPassword(string? passToValidate, User user)
        {
            if (string.IsNullOrWhiteSpace(passToValidate)) return false;
            if (user == null) return false;
            if (string.IsNullOrWhiteSpace(user?.PasswordHash)) return false;

            var passwordHashed = CryptoHelper.Hash(passToValidate) ?? "";

            return passwordHashed.Equals(user.PasswordHash);

        }

        public void UpdateUserRoles(string userId, List<Role> roles)
        {
            // TODO: update user roles
            throw new NotImplementedException();
        }

        public void UpdateUserPassword(PasswordChangeDto passwordChange)
        {
            // TODO: implement update password feature
            throw new NotImplementedException();
        }

        public bool ResetPassword(PasswordChangeDto login, string recoveryToken)
        {
            // TODO: implement reset password feature
            throw new NotImplementedException();
        }

        public bool RevokeToken(string token)
        {
            // TODO: implement revoke authToken access feature
            throw new NotImplementedException();
        }
        public JwtResponse SelectCompania(string id)
        {
            if (CurrentUser is null)
                throw new UnauthorizedException();

            var sucursal = UserCompanies.Find(x => x.Id == id && x.UserId == CurrentUser.Id) ??
                throw new NotFoundException();

            var tokenHandler = new JwtSecurityTokenHandler();

            var userToken = tokenHandler.ReadJwtToken(Session);

            var claims = userToken.Claims;

            var curSucursal = userToken.Claims.FirstOrDefault(x => x.Type == CustomJwtClaimTypes.Company);
            if (curSucursal != null)
                claims = claims.Except([curSucursal]);

            var token = GenerateUserToken(CurrentUser, null, [.. claims, new Claim(CustomJwtClaimTypes.Company, id)], out JwtSecurityToken? securityToken);

            UserTokens.SaveToken(CurrentUser, securityToken!, token, null);

            return new JwtResponse()
            {
                Token = token,
                Expiration = securityToken!.ValidTo.ToLocalTime()
            };
        }

        public JwtResponse SelectDireccionInstitucional(string id)
        {
            if (CurrentUser is null)
                throw new UnauthorizedException();

            var sucursal = userDireccionInstitucionalRepo.Find(x => x.Id == id && x.UserId == CurrentUser.Id) ??
                throw new NotFoundException();

            var tokenHandler = new JwtSecurityTokenHandler();

            var userToken = tokenHandler.ReadJwtToken(Session);

            var claims = userToken.Claims;

            var curSucursal = userToken.Claims.FirstOrDefault(x => x.Type == CustomJwtClaimTypes.DireccionInstitucional);
            if (curSucursal != null)
                claims = claims.Except([curSucursal]);

            var token = GenerateUserToken(CurrentUser, null, [.. claims, new Claim(CustomJwtClaimTypes.DireccionInstitucional, id)], out JwtSecurityToken? securityToken);

            UserTokens.SaveToken(CurrentUser, securityToken!, token, null);

            return new JwtResponse()
            {
                Token = token,
                Expiration = securityToken!.ValidTo.ToLocalTime()
            };
        }

        public JwtResponse SelectSucursal(string id)
        {
            if (CurrentUser is null)
                throw new UnauthorizedException();

            var sucursal = userSucursalRepo.Find(x => x.Id == id && x.UserId == CurrentUser.Id) ??
                throw new NotFoundException();

            var tokenHandler = new JwtSecurityTokenHandler();

            var userToken = tokenHandler.ReadJwtToken(Session);

            var claims = userToken.Claims;

            var curSucursal = userToken.Claims.FirstOrDefault(x => x.Type == CustomJwtClaimTypes.BranchOffice);
            if (curSucursal != null)
                claims = claims.Except([curSucursal]);

            var token = GenerateUserToken(CurrentUser, null, [.. claims, new Claim(CustomJwtClaimTypes.BranchOffice, id)], out JwtSecurityToken? securityToken);

            UserTokens.SaveToken(CurrentUser, securityToken!, token, null);

            return new JwtResponse()
            {
                Token = token,
                Expiration = securityToken!.ValidTo.ToLocalTime()
            };
        }

    }
}
