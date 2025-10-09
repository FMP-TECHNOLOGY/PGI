using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Login
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        //public string? CompanyRNC { get; set; }
    }
    public class RegisterDto
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
