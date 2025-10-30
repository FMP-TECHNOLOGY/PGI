using API_PGI.Auth;
using DataAccess;
using DataAccess.Repositories;
using Gridify;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.DireccionIntitucional
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
    public class DireccionIntitucionalController : ControllerBase
    {
        private readonly IDireccionIntitucional _DireccionIntitucionales;
        private IAuth _Auth { get; }

        public DireccionIntitucionalController(IDireccionIntitucional direccionIntitucional, IAuth auth)
        {
            _DireccionIntitucionales = direccionIntitucional;
            _Auth = auth;
        }



        [HttpPost]
        public virtual IActionResult post([FromBody] DataAccess.Entities.DireccionIntitucional entity)
        {
            try
            {
                _DireccionIntitucionales.AddSaving(entity);


                return Ok(new ResponseModel()
                {
                    Result = "Guardado"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel()
                {
                    Error = true,
                    Message = ex.Message
                });
            }

        }
        [HttpPut]
        public virtual IActionResult Put([FromBody] DataAccess.Entities.DireccionIntitucional entity)
        {
            try
            {
                _DireccionIntitucionales.UpdateSaving(entity);

                return Ok(new ResponseModel()
                {
                    Result = "Guardado"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel()
                {
                    Error = true,
                    Message = ex.Message
                });
            }

        }
        [HttpGet("GetAll")]
        public virtual IActionResult GetAll([FromQuery] GridifyQuery gridifyQuery)
        {
            try
            {
                var builder = new QueryBuilder<DataAccess.Entities.DireccionIntitucional>()
                             .AddQuery(gridifyQuery)
                             .AddCondition($"{nameof(BaseSystemData.CompaniaId)}={_Auth.CurrentUser?.CompaniaId}")
                ;
                if (gridifyQuery.PageSize == 0) gridifyQuery.PageSize = int.MaxValue;
                if (gridifyQuery.Page == 0) gridifyQuery.Page = 1;

                var items = _DireccionIntitucionales.GetPaginated(gridifyQuery);
                return Ok(new ResponseModel()
                {

                    TotalCount = items.Count,
                    Result = items.Data,
                });
                //  }
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel()
                {

                    Error = true,
                    Result = ex.Message
                });
            }

        }
        [HttpGet("{id}")]
        public virtual IActionResult GetAll(string id)
        {
            try
            {
                var valor = _DireccionIntitucionales.Find(x => x.Id == id);

                return Ok(new ResponseModel()
                {
                    Result = valor
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel()
                {
                    Error = true,
                    Message = ex.Message
                });
            }


        }

    }
}
