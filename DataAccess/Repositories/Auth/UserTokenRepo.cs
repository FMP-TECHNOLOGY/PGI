using PGI.Common.Utils;
using PGI.DataAccess.DbContenxts;
using PGI.DataAccess.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IUserToken : IGenericRepo<UserToken>
    {
        UserToken SaveToken(User user, JwtSecurityToken securityToken, string? token = null, string? host = null);
    }

    public class UserTokenRepo : GenericRepo<UserToken>, IUserToken
    {
        public UserTokenRepo(AppDBContext context) : base(context)
        {
        }

        public UserToken SaveToken(User user, JwtSecurityToken securityToken, string? token = null, string? host = null)
        {
            return AddSaving(new UserToken()
            {
                UserId = user.Id,
                AccessToken = string?.IsNullOrWhiteSpace(token)
                            ? new JwtSecurityTokenHandler().WriteToken(securityToken)
                            : token,
                Jti = securityToken?.Id,
                Hash = CryptoHelper.Hash(token),
                Typ = securityToken!.Header.Typ,
                Alg = securityToken!.SignatureAlgorithm,
                Exp = securityToken.ValidTo,
                Host = host
            });
        }
    }
}
