using API_PGI.Auth;
using DataAccess.Entities;
using DataAccess.Repositories;
using Gridify;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.Parametros
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
    public class ParametroController : ControllerBase
    {
        private readonly IParametro _Parametro;
        private IAuth _Auth { get; }

        public ParametroController(IParametro Parametro, IAuth auth)
        {
            _Parametro = Parametro;
            _Auth = auth;
        }

        [HttpPost]
        public IActionResult post([FromBody] Parametro entity)
        {
            try
            {
                _Parametro.AddSaving( entity);


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
        public IActionResult Put([FromBody] Parametro entity)
        {
            try
            {

                _Parametro.UpdateSaving( entity);

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
        public IActionResult GetAll([FromQuery] GridifyQuery gridifyQuery)
        {
            try
            {
                var builder = new QueryBuilder<Parametro>()
                             .AddQuery(gridifyQuery)
                             .AddCondition($"{nameof(Parametro.CompaniaId)}={_Auth.CurrentUser?.CompaniaId}")
                ;
                if (gridifyQuery.PageSize <= 0) gridifyQuery.PageSize = int.MaxValue;
                if (gridifyQuery.Page <= 0) gridifyQuery.Page = 1;

                var items = _Parametro.GetPaginated(gridifyQuery);
                return Ok(new ResponseModel()
                {
                    PageNumber = gridifyQuery.Page,
                    Rows = items.Data.Count(),
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
        [HttpGet("Get/{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var valor = _Parametro.Find(x => x.Id == id);

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
