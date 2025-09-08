using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public bool Sudo { get; set; } = false;

        public readonly string[] Roles;

        public readonly bool AllowAnonymous;

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
            if (AllowAnonymous) return;

            var authService = context.HttpContext.RequestServices.GetService<IAuth>()
                ?? throw new CustomException(500, $"Fault on get auth service", "50x005");

            var user = authService.CurrentUser
               ?? throw new CustomException(401, "Unautorized", "40x041", "no se pudo obtener el usuario en el atributo de autorizacion");

            // logged in
            if (user.LockoutEnabled)
                throw new CustomException(403, "Unautorized - User Locked", "40x042", "el usuario esta bloqueado");

            if (Sudo && user.Su == false)
                throw new CustomException(403, "Forbbiden", "40x043", "el usuario sin acceso a sudo intento acceder a una ruta sudo");


            // logged in
            //if (Roles != null)
            //{
            //    var inRole = user.Roles
            //            .Where(role => Roles.Select(r => r.ToUpper().Trim())
            //            .Contains(role.Name.ToUpper().Trim()))
            //            .ToList().Count > 0;

            //    if (!inRole)
            //    {
            //        // Forbidden
            //        context.Result = new JsonResult(ResponseModel.GetForbbidenResponse());
            //        context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            //    }
            //}

        }

    }
}
