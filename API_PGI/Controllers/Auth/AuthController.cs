using API_PGI.Auth;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.Auth
{
    public partial class AuthController : ControllerBase
    {
        private readonly IAuth auth;

        public AuthController(IAuth auth)
        {
            this.auth = auth;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">User credentials</param>
        /// <returns><seealso cref="ResponseModel{JwtResponse}"/> Jwt Token</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [HttpPost("login"), JwtAuthorize(AllowAnonymous = true)]
        public ActionResult<ResponseModel<JwtResponse>> Login([FromBody] Login login)
        {
            var loginTask = auth.Login(login, Request.HttpContext.Connection.RemoteIpAddress?.ToString());
            if (loginTask.IsCompletedSuccessfully)
                return Ok(new ResponseModel<JwtResponse>(loginTask.Result));

            return Unauthorized(ResponseModel.GetUnauthorizedResponse(loginTask.Exception?.InnerException?.Message));
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="register">User register form</param>
        /// <returns></returns>
        /// <returns><seealso cref="ResponseModel{JwtResponse}"/> Jwt Token</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [HttpPost("register")]
        public ActionResult<ResponseModel> Register([FromBody] RegisterDto register)
        {
            var registerTask = auth.Register(register);
            if (registerTask.IsCompletedSuccessfully)
                return Ok(new ResponseModel("User created successfully"));

            return BadRequest(new ResponseModel(registerTask.Exception?.InnerException?.Message));
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <returns></returns>
        /// <returns><seealso cref="ResponseModel{JwtResponse}"/> Jwt Token</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [JwtAuthorize]
        [HttpGet("refresh-token")]
        public ActionResult<ResponseModel> Register()
        {
            return Ok(new ResponseModel<JwtResponse>(auth.RefreshToken(Request.HttpContext.Connection.RemoteIpAddress?.ToString()).Result));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [JwtAuthorize]
        [HttpPost("Select/Compania/{Id}")]
        public ActionResult<ResponseModel> SelectCompania([FromRoute] string Id)
        {
            return Ok(auth.SelectCompania(Id));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [JwtAuthorize]
        [HttpPost("Select/DireccionInstitucional/{Id}")]
        public ActionResult<ResponseModel> SelectDireccionInstitucional([FromRoute] string Id)
        {
            
            return Ok(auth.SelectDireccionInstitucional(Id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]
        [JwtAuthorize]
        [HttpPost("Select/Sucursal/{Id}")]
        public ActionResult<ResponseModel> SelectSucursal([FromRoute] string Id)
        {
            
            return Ok(auth.SelectSucursal(Id));
        }

    }
}
