using API_PGI.Auth;
using DataAccess.Entities;
using DataAccess.Repositories;
using Gridify;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.Xpaccs
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
    public class XpaccController : ControllerBase
    {
        private readonly IXpacc _Xpacc;
        private IAuth _Auth { get; }

        public XpaccController(IXpacc Xpacc, IAuth auth)
        {
            _Xpacc = Xpacc;
            _Auth = auth;
        }

        [HttpPost]
        public IActionResult post([FromBody] Xpacc entity)
        {
            try
            {
                _Xpacc.AddSaving( entity);


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
        public IActionResult Put([FromBody] Xpacc entity)
        {
            try
            {

                _Xpacc.UpdateSaving( entity);

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
                var builder = new QueryBuilder<Xpacc>()
                             .AddQuery(gridifyQuery)
                             .AddCondition($"{nameof(Xpacc.CompaniaId)}={_Auth.CurrentUser?.CompaniaId}")
                ;
                if (gridifyQuery.PageSize == 0) gridifyQuery.PageSize = int.MaxValue;
                if (gridifyQuery.Page == 0) gridifyQuery.Page = 1;

                var items = _Xpacc.FindAll(gridifyQuery);
                return Ok(new ResponseModel()
                {

                    TotalCount = items.Count,
                    Result = items,
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
                var valor = _Xpacc.Find(x => x.Id == id);

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
