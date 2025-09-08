using API_PGI.Auth;
using DataAccess.Entities;
using DataAccess.Repositories;
using Gridify;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.Accions
{
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Route("[controller]")]
    //[ApiController]
    //[Authorize]
    //public class AccionController : ControllerBase
    //{
    //    private readonly IAccion _Accion;
    //    private IAuth _Auth { get; }

    //    public AccionController(IAccion accion, IAuth auth)
    //    {
    //        _Accion = accion;
    //        _Auth = auth;
    //    }

    //    [HttpPost]
    //    public IActionResult post([FromBody] Accion entity)
    //    {
    //        try
    //        {
    //            _Accion.AddSaving( entity);


    //            return Ok(new ResponseModel()
    //            {
    //                Result = "Guardado"
    //            });
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(new ResponseModel()
    //            {
    //                Error = true,
    //                Message = ex.Message
    //            });
    //        }

    //    }
    //    [HttpPut]
    //    public IActionResult Put([FromBody] Accion entity)
    //    {
    //        try
    //        {

    //            _Accion.UpdateSaving( entity);

    //            return Ok(new ResponseModel()
    //            {
    //                Result = "Guardado"
    //            });
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(new ResponseModel()
    //            {
    //                Error = true,
    //                Message = ex.Message
    //            });
    //        }

    //    }
    //    [HttpGet("GetAll")]
    //    public IActionResult GetAll([FromQuery] GridifyQuery gridifyQuery)
    //    {
    //        try
    //        {
    //            var builder = new QueryBuilder<Accion>()
    //                         .AddQuery(gridifyQuery)
    //                         .AddCondition($"{nameof(Accion.CompaniaId)}={_Auth.CurrentUser?.CompaniaId}")
    //            ;
    //            if (gridifyQuery.PageSize == 0) gridifyQuery.PageSize = int.MaxValue;
    //            if (gridifyQuery.Page == 0) gridifyQuery.Page = 1;

    //            var items = _Accion.GetPaginated(gridifyQuery);
    //            return Ok(new ResponseModel()
    //            {
    //                TotalCount = items.Count,
    //                Result = items.Data,
    //            });
    //            //  }
    //        }
    //        catch (Exception ex)
    //        {

    //            return BadRequest(new ResponseModel()
    //            {

    //                Error = true,
    //                Result = ex.Message
    //            });
    //        }

    //    }
    //    [HttpGet("Get/{id}")]
    //    public IActionResult GetAll(string id)
    //    {
    //        try
    //        {
    //            var valor = _Accion.Find(x => x.Id == id);

    //            return Ok(new ResponseModel()
    //            {
    //                Result = valor
    //            });
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(new ResponseModel()
    //            {
    //                Error = true,
    //                Message = ex.Message
    //            });
    //        }


    //    }
    //}
}
