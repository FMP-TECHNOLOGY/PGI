using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API_PGI.Controllers.Auth
{
    public partial class AuthController : ControllerBase
    {
        /// <summary>
        /// Get company users
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public ActionResult<ResponseModel<List<User>>> GetUsers()
        {
            var CompaniaId = auth.CurrentUser?.CompaniaId;
            return Ok(new ResponseModel<List<User>>(auth.Users.FindAll(x => x.CompaniaId == CompaniaId)));

        }

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns><seealso cref="ResponseModel{User}"/> User</returns>
        [HttpGet("users/{username}")]
        public ActionResult<ResponseModel<User>> GetUsers([FromRoute] string username)
            => Ok(new ResponseModel<User>(auth.Users.Find(x => username.Equals(x.Username, StringComparison.InvariantCultureIgnoreCase))));

        /// <summary>
        /// Create user permission
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="permissionId">RequiredPermission Id</param>
        /// <returns></returns>
        [HttpPost("users/{username}/permissions/{permissionId}")]
        public ActionResult<ResponseModel> PostUserPermission([FromRoute] string username, [FromRoute] string permissionId)
        {
            auth.UserPermissions.AddPermission(username, permissionId);
            return Created(string.Empty, new ResponseModel());
        }

        /// <summary>
        /// Remove user permission
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="permissionId">RequiredPermission Id</param>
        /// <returns></returns>
        [HttpDelete("users/{username}/permissions/{permissionId}")]
        public ActionResult<ResponseModel> RemoveUserPermission([FromRoute] string username, [FromRoute] string permissionId)
        {
            auth.UserPermissions.RemovePermission(username, permissionId);
            return Ok(new ResponseModel());
        }
    }


}
