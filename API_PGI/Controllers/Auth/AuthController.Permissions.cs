using API_PGI.Auth;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API_PGI.Controllers.Auth
{
    public partial class AuthController : ControllerBase
    {

        /// <summary>
        /// Get permissions
        /// </summary>
        /// <returns></returns>
        /// <returns><seealso cref="ResponseModel{Role}"/>RequiredPermission</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [HttpGet("permissions")]
        public ActionResult<ResponseModel<List<Permission>>> GetPermissions()
            => Ok(new ResponseModel<List<Permission>>(auth.Permissions.GetAll()));

        /// <summary>
        /// Create permission
        /// </summary>
        /// <param name="permission">RequiredPermission register form</param>
        /// <returns></returns>
        /// <returns><seealso cref="ResponseModel{Permission}"/>RequiredPermission</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [HttpPost("permissions"), JwtAuthorize(SuperUserRequired = true)]
        public ActionResult<ResponseModel<Role>> PostPermission(Permission permission)
            => Created(string.Empty, new ResponseModel<Permission>(auth.Permissions.AddSaving(permission)));
    }
}
