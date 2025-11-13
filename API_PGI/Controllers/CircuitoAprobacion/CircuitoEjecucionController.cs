using API_PGI.Auth;
using DataAccess.Entities;
using DataAccess.Repositories;
using Gridify;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.CircuitoAprobacion
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
    public class CircuitoEjecucionController : ControllerBase
    {
        private readonly ICircuitoEjecucion _Repo;
        private IAuth _Auth { get; }

        public CircuitoEjecucionController(ICircuitoEjecucion BaseSystemData, IAuth auth)
        {
            _Repo = BaseSystemData;
            _Auth = auth;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CircuitoEjecucion entity)
        {
            try
            {
                _Repo.AddSaving(entity);
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
        public IActionResult Put([FromBody] CircuitoEjecucion entity)
        {
            try
            {
                _Repo.UpdateSaving(entity);
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
                var builder = new QueryBuilder<CircuitoEjecucion>()
                             .AddQuery(gridifyQuery)
                             .AddCondition($"{nameof(CircuitoEjecucion.CompaniaId)}={_Auth.CurrentUser?.CompaniaId}");

                if (gridifyQuery.PageSize == 0) gridifyQuery.PageSize = int.MaxValue;
                if (gridifyQuery.Page == 0) gridifyQuery.Page = 1;

                var items = _Repo.GetPaginated(gridifyQuery);
                return Ok(new ResponseModel()
                {
                    TotalCount = items.Count,
                    Result = items.Data,
                });
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
        public IActionResult Get(string id)
        {
            try
            {
                var valor = _Repo.Find(x => x.Id == id);
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
