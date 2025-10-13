
using Auth.Claims;
using Common.Exceptions;
using DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataAccess.Builders
{
    public class UserTokenBuilder
    {
        private User? user;

        private readonly SecurityTokenDescriptor tokenDescriptor;

        public string TokenGuid { get; init; } = Guid.NewGuid().ToString();

        public UserTokenBuilder()
        {
            tokenDescriptor = new();

            SetExpirity(hour: 1);
        }

        public UserTokenBuilder(User user, bool setBasicClaims = true)
        {
            this.user = user ?? throw new BadRequestException($"Invalid parameter {nameof(user)}");

            tokenDescriptor = new();

            if (setBasicClaims)
                SetBasicClaims();

            SetCompany(user.Company?.Id.ToString());

            SetExpirity(hour: 1);

            SetSigningCredentials(user.PasswordHash);
        }

        public UserTokenBuilder SetUser(User? user)
        {
            this.user = user;

            if (user is null)
                RemoveClaims(JwtRegisteredClaimNames.UniqueName, CustomJwtClaimTypes.UserId);
            else
            {
                SetClaims(
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                    new Claim(CustomJwtClaimTypes.UserId, user.Id.ToString()));

                SetExpirity(hour: 1);

                SetSigningCredentials(user.PasswordHash);
            }

            return this;
        }

        public UserTokenBuilder SetCompany(string? company)
        {
            if (string.IsNullOrWhiteSpace(company))
                return RemoveClaims(CustomJwtClaimTypes.Company);

            return SetClaims(new Claim(CustomJwtClaimTypes.Company, company));
        }

        public UserTokenBuilder SetHost(string? host)
        {
            tokenDescriptor.Audience = host;

            return this;
        }

        public UserTokenBuilder SetBasicClaims()
            => SetClaims(
                new Claim(JwtRegisteredClaimNames.UniqueName, user?.Username),
                new Claim(JwtRegisteredClaimNames.Jti, TokenGuid),
                new Claim(CustomJwtClaimTypes.UserId, user?.Id.ToString()));

        public UserTokenBuilder SetExpirity(
            int? years = null,
            int? months = null,
            int? days = null,
            int? hour = 1,
            int? minutes = null,
            int? seconds = null)
        {
            var expirity = DateTime.UtcNow;

            if (years.HasValue)
                expirity = expirity.AddYears(years.Value);

            if (months.HasValue)
                expirity = expirity.AddMonths(months.Value);

            if (days.HasValue)
                expirity = expirity.AddDays(days.Value);

            if (hour.HasValue)
                expirity = expirity.AddHours(hour.Value);

            if (minutes.HasValue)
                expirity = expirity.AddMinutes(minutes.Value);

            if (seconds.HasValue)
                expirity = expirity.AddSeconds(seconds.Value);

            tokenDescriptor.Expires = expirity;

            return this;
        }

        public UserTokenBuilder SetExpirity(DateTime dateTime)
        {
            tokenDescriptor.Expires = dateTime;

            return this;
        }

        public UserTokenBuilder SetIssuer(string issuer)
        {
            tokenDescriptor.Issuer = issuer;
            return this;
        }

        public UserTokenBuilder SetSigningCredentials(string password, string algorithm = SecurityAlgorithms.HmacSha512)
        {

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            if (string.IsNullOrWhiteSpace(algorithm))
                throw new ArgumentNullException(nameof(algorithm));

            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(password)), algorithm);
            return this;
        }

        public JwtSecurityToken Build(string? password = null, string algorithm = SecurityAlgorithms.HmacSha512)
        {
            if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(algorithm))
                SetSigningCredentials(password, algorithm);

            return new JwtSecurityTokenHandler().CreateJwtSecurityToken(tokenDescriptor);
        }

        public UserTokenBuilder SetClaims(params Claim[] claims)
        {
            var oldClaims = tokenDescriptor.Subject?.Claims ?? Array.Empty<Claim>();
            var newClaims = new List<Claim>(oldClaims);

            newClaims.AddRange(claims);

            newClaims = newClaims.GroupBy(e => e.Type)
                                 .Select(g => g.OrderByDescending(e => e.Type).Last())
                                 .ToList();

            tokenDescriptor.Subject = new ClaimsIdentity(newClaims);

            return this;
        }

        private UserTokenBuilder RemoveClaims(params string[] claims)
        {
            var subjectClaims = tokenDescriptor.Subject?.Claims ?? Array.Empty<Claim>();

            var newClaims = subjectClaims.ExceptBy(claims, e => e.Type)?.ToArray() ?? Array.Empty<Claim>();

            return SetClaims(newClaims);
        }
    }

}
