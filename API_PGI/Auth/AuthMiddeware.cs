using Common.Constants;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Auth
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate next;

        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context, IAuth auth)
        {
            var token = context.Request.Headers[AppConstants.AUTHORIZATION]
                .FirstOrDefault()?
                .Split(" ", 2, StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault();

            if (!string.IsNullOrWhiteSpace(token))            
                auth.SetCurrentCredentials(token, AppConstants.BEARER_TOKEN, context.Connection.RemoteIpAddress);
                
            await next(context);
        }

    }
}
