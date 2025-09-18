using API_PGI.Auth;
using DataAccess.Entities;
using DataAccess.Repositories;
using Gridify;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.Companias
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
    public class CompaniaController : ControllerBase
    {
        private readonly ICompania _Compania;
        private IAuth _Auth { get; }

        public CompaniaController(ICompania Compania, IAuth auth)
        {
            _Compania = Compania;
            _Auth = auth;
        }

        [HttpPost]
        public IActionResult post([FromBody] Compania entity)
        {
            try
            {
                _Compania.AddSaving( entity);


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
        public IActionResult Put([FromBody] Compania entity)
        {
            try
            {

                _Compania.UpdateSaving( entity);

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
                var builder = new QueryBuilder<Compania>()
                             .AddQuery(gridifyQuery)
                             //.AddCondition($"{nameof(Compania.CompaniaId)}={_Auth.CurrentUser?.CompaniaId}")
                ;
                if (gridifyQuery.PageSize == 0) gridifyQuery.PageSize = int.MaxValue;
                if (gridifyQuery.Page == 0) gridifyQuery.Page = 1;

                var items = _Compania.FindAll(gridifyQuery);
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
                var valor = _Compania.Find(x => x.Id.ToString() == id);

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
