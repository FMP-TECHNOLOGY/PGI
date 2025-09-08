
namespace Common.Exceptions
{
    public class LoginException : CustomException
    {

        public LoginException() : base("Invalid username or password")
        {
        }
        public LoginException(string? message) : base(message)
        {
        }

    }
}
