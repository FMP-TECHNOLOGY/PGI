using API_PGI.Auth;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API_PGI.Controllers.Auth
{
    public partial class AuthController : ControllerBase
    {

        /// <summary>
        /// Get roles
        /// </summary>
        /// <returns></returns>
        /// <returns><seealso cref="ResponseModel{Role}"/>Role</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [HttpGet("roles")]
        public ActionResult<ResponseModel<List<Role>>> GetRoles()
            => Ok(new ResponseModel<List<Role>>(auth.Roles.GetAll()));

        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="permission">Role register form</param>
        /// <returns></returns>
        /// <returns><seealso cref="ResponseModel{Role}"/>Role</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [HttpPost("roles"), JwtAuthorize(SuperUserRequired = true)]
        public ActionResult<ResponseModel<Role>> PostRole(Role permission)
            => Created(string.Empty, new ResponseModel<Role>(auth.Roles.AddSaving(permission)));

        /// <summary>
        /// Create Role Permission
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [HttpPost("roles/{roleId}/permissions/{permissionId}"), JwtAuthorize(SuperUserRequired = true)]
        public ActionResult<ResponseModel> PostRolePermission([FromRoute] string roleId, [FromRoute] string permissionId)
        {
            auth.RolePermissions.AddPermission(roleId, permissionId);
            return Created(string.Empty, new ResponseModel());
        }

        /// <summary>
        /// Remove Role Permission
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [HttpDelete("roles/{roleId}/permissions/{permissionId}"), JwtAuthorize(SuperUserRequired = true)]
        public ActionResult<ResponseModel> RemoveRolePermission([FromRoute] string roleId, [FromRoute] string permissionId)
        {
            auth.RolePermissions.RemovePermission(roleId, permissionId);
            return Ok(new ResponseModel());
        }
    }
}
