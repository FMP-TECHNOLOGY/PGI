using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class DetalleRequisicion : IUserIdentity, IIdentity
    {
        public string? Id { get; set; }
        public string? RequisicionId { get; set; }
        public string? CuentaObjetal { get; set; }
        public string? ProductoId { get; set; }
        public int? Cantidad { get; set; }
        public int? NumeroProceso { get; set; }
        public int? EstadoId { get; set; }
        public int? LineNumRequisicion { get; set; }
        public string? UserId { get; set; }
        public DateTime? Created { get; set; }
        public int? ObjectType { get; set; }
    }
}
