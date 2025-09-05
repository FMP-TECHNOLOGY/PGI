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
            //new Log().Error(context.Connection.RemoteIpAddress?.ToString());

            var token = context.Request.Headers[AppConstants.AUTHORIZATION]
                .FirstOrDefault()?
                .Split(" ", 2, StringSplitOptions.RemoveEmptyEntries)
                .Last();
            //new Log().Error(token);

            if (!string.IsNullOrWhiteSpace(token))
            {
                //new Log().Error(token);

                var user = auth.FindUserByToken(token, context.Connection.RemoteIpAddress?.ToString());
                //new Log().Error(user?.Username);

                //var user = auth.FindUserByToken(token, context.Request.HttpContext.Connection.RemoteIpAddress?.ToString());
                //new Log().Error(context.Connection.RemoteIpAddress?.ToString());
                if (user != null && auth.IsValidToken(token))
                    auth.CurrentUser = user;

            }

            await next(context);
        }

    }
}
