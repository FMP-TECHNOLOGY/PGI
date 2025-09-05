using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.JWT
{
    public class JwtConfig
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }

        public TokenValidationParameters Parameters => new()
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)),
            ValidIssuer = Issuer,
            ClockSkew = TimeSpan.Zero,
        };
    }
}
