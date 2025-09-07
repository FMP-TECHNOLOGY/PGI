using API_PGI.Auth;
using DataAccess.Entities;
using DataAccess.Repositories;
using Gridify;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.Xxactividades
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class XxactividadeController : ControllerBase
    {
        private readonly IXxactividade _Xxactividade;
        private IAuth _Auth { get; }

        public XxactividadeController(IXxactividade Xxactividade, IAuth auth)
        {
            _Xxactividade = Xxactividade;
            _Auth = auth;
        }

        [HttpPost]
        public IActionResult post([FromBody] Xxactividade entity)
        {
            try
            {
                _Xxactividade.AddSaving(_Auth.CurrentUser, entity);


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
        public IActionResult Put([FromBody] Xxactividade entity)
        {
            try
            {

                _Xxactividade.UpdateSaving(_Auth.CurrentUser, entity);

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
                var builder = new QueryBuilder<Xxactividade>()
                             .AddQuery(gridifyQuery)
                             .AddCondition($"{nameof(Xxactividade.CompaniaId)}={_Auth.CurrentUser?.CompaniaId}")
                ;
                if (gridifyQuery.PageSize == 0) gridifyQuery.PageSize = int.MaxValue;
                if (gridifyQuery.Page == 0) gridifyQuery.Page = 1;

                var items = _Xxactividade.GetPaginated(gridifyQuery);
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
        [HttpGet("Get/{id}")]
        public IActionResult GetAll(string id)
        {
            try
            {
                var valor = _Xxactividade.Find(x => x.Id == id);

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
