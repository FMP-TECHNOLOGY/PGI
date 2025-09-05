using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class JwtResponse
    {
        public string?Token { get; set; }
        public DateTimeOffset Expiration { get; set; }
    }
}
