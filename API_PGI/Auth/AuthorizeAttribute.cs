using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] Roles;

        private readonly bool AllowAnonymous;

        public AuthorizeAttribute()
        {

        }

        public AuthorizeAttribute(string Roles)
        {
            this.Roles = Roles.Split(",", StringSplitOptions.RemoveEmptyEntries);
        }

        public AuthorizeAttribute(bool allowAnonymous)
        {
            AllowAnonymous = allowAnonymous;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var isAllowed = context.ActionDescriptor.EndpointMetadata
                .Where(x => x is AuthorizeAttribute)
                .Select(x => x as AuthorizeAttribute)
                    .Where(x => x.AllowAnonymous)
                    .Select(x => x.AllowAnonymous)
                .FirstOrDefault();

            if (AllowAnonymous || isAllowed) return;

            var user = context.HttpContext.RequestServices.GetService<IAuth>()?.CurrentUser;

            if (user == null)
            {
                // not logged in
                //new Log().Info("Joder");
                //throw new Exception();
                context.Result = new JsonResult(ResponseModel.GetUnauthorizedResponse());
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            }
            else
            {
                // logged in
                if (Roles != null)
                {
                    var inRole = user.Roles
                            .Where(role => Roles.Select(r => r.ToUpper().Trim())
                            .Contains(role.Name.ToUpper().Trim()))
                            .ToList().Count > 0;

                    if (!inRole)
                    {
                        // Forbidden
                        context.Result = new JsonResult(ResponseModel.GetForbbidenResponse());
                        context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    }
                }
            }
        }

    }
}
