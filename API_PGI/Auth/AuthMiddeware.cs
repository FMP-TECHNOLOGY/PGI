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
            Auth(context, auth);

            await next(context);
        }

        private static void Auth(HttpContext context, IAuth auth)
        {
            var authHeader = context.GetAuthorization(out string location);

            if (string.IsNullOrWhiteSpace(authHeader))
                return;

            var userAgent = context.Request.GetTypedHeaders().Headers.UserAgent.SingleOrDefault();

            auth.SetCurrentCredentials(authHeader, location, context.Connection.RemoteIpAddress, userAgent);
        }

    }
}
