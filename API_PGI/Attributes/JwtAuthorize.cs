using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_PGI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorize : Attribute, IAuthorizationFilter
    {
        public string?RequiredPermission { get; set; } = string?.Empty;
        public bool AllowAnonymous { get; set; }
        public bool SuperUserRequired { get; set; }
        public bool ValidCompanyRequired { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (AllowAnonymous) return;

            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor
                && (controllerActionDescriptor.MethodInfo.GetCustomAttribute<JwtAuthorize>()?.AllowAnonymous ?false))
                return;

            var authService = context.HttpContext.RequestServices.GetService<IAuth>()
                ?throw new Exception($"Cannot get {nameof(IAuth)} service");

            var user = authService.CurrentUser
                ?throw new UnauthorizedException();

            ValidateCompany(authService);

            if (SuperUserRequired && !user.Su)
                throw new ForbiddenException();

            if (user.Su || string?.IsNullOrWhiteSpace(RequiredPermission))
                return;

            var hasPermission = user.Permissions.Any(permission => permission.Id == RequiredPermission);

            if (hasPermission) return;

            throw new ForbiddenException();
        }

        private void ValidateCompany(IAuth authService)
        {
            if (!ValidCompanyRequired)
                return;

            if (authService.CurrentCompany is null)
                throw new BadRequestException("Invalid company please login again");

            if (!authService.CurrentCompany.Active)
                throw new BadRequestException("Company inactive");
        }
    }
}
