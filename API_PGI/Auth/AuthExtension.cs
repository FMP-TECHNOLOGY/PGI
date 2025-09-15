using Common.Constants;
using Microsoft.Extensions.Primitives;

namespace API_PGI.Auth
{
    public static class AuthExtension
    {
        public static string? GetAuthorization(this HttpContext context, out string location)
        {
            location = string.Empty;

            if (context == null) return null;

            return GetAuthorization(context.Request, out location);
        }

        public static string? GetAuthorization(this HttpRequest context, out string location)
        {
            location = string.Empty;

            if (context == null) return null;

            if (context.HttpContext.Request.Headers.TryGetValue(AppConstants.AUTHORIZATION, out StringValues values))
            {
                location = AppConstants.AUTHORIZATION;
                return values.FirstOrDefault();
            }

            if (context.HttpContext.Request.Query.TryGetValue(AppConstants.AUTHORIZATION, out values))
            {
                location = AppConstants.AUTHORIZATION;
                return values.FirstOrDefault();
            }

            if (context.HttpContext.Request.Query.TryGetValue(AppConstants.API_KEY_TOKEN, out values))
            {
                location = AppConstants.API_KEY_TOKEN;
                return values.FirstOrDefault();
            }

            if (context.HttpContext.Request.Headers.TryGetValue(AppConstants.API_KEY_TOKEN, out StringValues value))
            {
                location = AppConstants.API_KEY_TOKEN;
                return value.FirstOrDefault();
            }

            return null;
        }
    }
}
