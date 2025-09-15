
//using Gridify;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;

//namespace PGI.DataAccess.Repositories.Auth
//{
//    public interface IUserApiKey : IGenericRepo<UserApiKey>
//    {
//        List<UserApiKey> GetUserApiKeysByCurrentUser(GridifyQuery? query = null);
//    }

//    public partial class UserApiKeyRepo : GenericRepo<UserApiKey>, IUserApiKey
//    {
//        private readonly JwtConfig _jwtConfig;
//        private readonly IUser _usersRepo;

//        public UserApiKeyRepo(AppDBContext context,
//                              IOptionsMonitor<JwtConfig> oJwtConfig,
//                              IUser usersRepo) : base(context)
//        {
//            _jwtConfig = oJwtConfig.CurrentValue;
//            _usersRepo = usersRepo;
//        }

//        public List<UserApiKey> GetUserApiKeysByCurrentUser(GridifyQuery? query = null)
//        {
//            var authManager = context.GetService<IAuth>()
//                ?throw new ArgumentNullException(nameof(IAuth));

//            if (authManager.CurrentUser is null || authManager.CurrentCompany is null)
//                throw new UnauthorizedException();

//            query ??= new();

//            return (from t0 in EntityDbSet
//                    where (authManager.CurrentUser.Su && t0.CompaniaId == authManager.CurrentCompany.Id)
//                    || (t0.UserId == authManager.CurrentUser.Id && t0.CompaniaId == authManager.CurrentCompany.Id)
//                    orderby t0.CreatedAt descending
//                    select t0
//                    )
//                    .Gridify(query).Data
//                   .ToList();

//        }

//        public override UserApiKey Add(UserApiKey entity)
//        {
//            var user = _usersRepo.FindValidById(entity.UserId)
//                ?throw new BadRequestException("Invalid user");

//            var company = user.Company?.Id == entity.CompaniaId
//                ? user.Company
//                : user.Companies.SingleOrDefault(x => x.Id == entity.CompaniaId)
//                ?throw new BadRequestException("Invalid company");

//            if (entity.AllowedIPs is not null
//                && !entity.AllowedIPs.All(x => RegExPatterns.IpAddressRegex().IsMatch(x)))
//                throw new BadRequestException("At least one of the following Ip addresses is not valid")
//                {
//                    Errors = entity.AllowedIPs.Where(x => !RegExPatterns.IpAddressRegex().IsMatch(x))
//                };

//            var securityToken = new UserTokenBuilder()
//                .SetUser(user)
//                .SetCompany(company.TaxIdNumber)
//                .SetExpirity(entity.NotAfterUTC ??= DateTime.UtcNow.AddYears(99))
//                .SetIssuer(_jwtConfig.Issuer)
//                .SetSigningCredentials(user.PasswordHash, SecurityAlgorithms.HmacSha256)
//                .Build();

//            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

//            entity.AccessToken = token;

//            return base.Add(entity);
//        }
//    }
//}
