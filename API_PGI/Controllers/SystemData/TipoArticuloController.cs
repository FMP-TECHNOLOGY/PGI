using API_PGI.Auth;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.SystemData
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("[controller]")]
    [ApiController]
    [JwtAuthorize]
    public class TipoArticuloController : BaseSystemDataController<TipoArticulo>
    {
        public TipoArticuloController(ISystemData<TipoArticulo> baseSystemData, IAuth auth) : base(baseSystemData, auth)
        {

        }
    }
}
