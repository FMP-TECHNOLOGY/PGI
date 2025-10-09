using API_PGI.Auth;
using DataAccess.Entities;
using DataAccess.Repositories;
using Gridify;
using Microsoft.AspNetCore.Mvc;
using Model;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.DocumentosEvidencias
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
    public class DocumentosEvidenciaController : ControllerBase
    {
        private readonly IDocumentosEvidencia _DocumentosEvidencia;
        private readonly ISolicitudCompra _SolicitudCompra;
        private IAuth _Auth { get; }
        private readonly string _basePath;

        public DocumentosEvidenciaController(IDocumentosEvidencia DocumentosEvidencia, IAuth auth, ISolicitudCompra solicitudCompra, IConfiguration config)
        {
            _DocumentosEvidencia = DocumentosEvidencia;
            _Auth = auth;
            _SolicitudCompra = solicitudCompra;
            _basePath = config["StoragePath"];
        }

        [HttpPost]
        public IActionResult post([FromForm] Archivo Anexos, string idDocumentoBase, int NoLinea, int ObjectType)
        {
            try
            {
                //var solicitud = _SolicitudCompra.Find(x => x.Id == SolicitudId);
                //if (solicitud == null)
                //{
                //    return BadRequest(new ResponseModel()
                //    {
                //        Message = "Documento no existe"
                //    });
                //}
                var empresaFolder = Path.Combine(_basePath, $"{_Auth.CurrentCompany.Id}", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));
                Directory.CreateDirectory(empresaFolder);

                List<DocumentosEvidencia> documentosEvidencias = new List<DocumentosEvidencia>();
                foreach (var item in Anexos.Archivos)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(item.FileName)}";
                    var filePath = Path.Combine(empresaFolder, fileName);

                    string extencion = item.ContentType.Split('/')[1];

                    //string nombre = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
                    //var ruta = Path.Combine(_hostingEnvironment.WebRootPath, "Imagenes", $"{nombre}.{extencion}");//; $"Imagenes/{nombre}.{extencion}";
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyToAsync(stream);
                    }
                    //ruta = Path.Combine("Imagenes", $"{nombre}.{extencion}");//; $"Imagenes/{nombre}.{extencion}";

                    documentosEvidencias.Add(new DocumentosEvidencia() { Extencion = extencion, IdDocumentoBase = idDocumentoBase, NombreArchivo = fileName, Path = filePath, CompaniaId = _Auth.CurrentCompany.Id, NoLinea = NoLinea, ObjectTypeBase = ObjectType, UserId = _Auth.CurrentUser.Id });
                }

                _DocumentosEvidencia.AddOrUpdateRange(documentosEvidencias);
                //dynamic items = Anexo.Solicitudprestamos.Get(id);

                return Ok(new ResponseModel()
                {
                    Result = "Guardado"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel()
                {
                    Message = ex.Message
                });
            }

        }
        [HttpPut]
        public IActionResult Put([FromBody] DocumentosEvidencia entity)
        {
            try
            {

                _DocumentosEvidencia.UpdateSaving(entity);

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
                var builder = new QueryBuilder<DocumentosEvidencia>()
                             .AddQuery(gridifyQuery)
                             .AddCondition($"{nameof(DocumentosEvidencia.CompaniaId)}={_Auth.CurrentUser?.CompaniaId}")
                ;
                if (gridifyQuery.PageSize == 0) gridifyQuery.PageSize = int.MaxValue;
                if (gridifyQuery.Page == 0) gridifyQuery.Page = 1;

                var items = _DocumentosEvidencia.GetPaginated(gridifyQuery);
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
                var valor = _DocumentosEvidencia.Find(x => x.Id == id);

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
        [HttpDelete("Archivo/id/{id}")]
        public IActionResult DeleteArchivo(string id)
        {
            try
            {
                var anexo = _DocumentosEvidencia.delete(id);

                //dynamic items = Anexo.Solicitudprestamos.Get(id);

                return Ok(new ResponseModel()
                {
                    Result = anexo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel()
                {
                    Message = ex.Message
                });
            }

        }
    }
}
