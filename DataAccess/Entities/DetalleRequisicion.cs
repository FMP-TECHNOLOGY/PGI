using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class DetalleRequisicion 
    {
        public string? Id { get; set; }
        public string? RequisicionId { get; set; }
        public string? CuentaObjetal { get; set; }
        public int? Cantidad { get; set; }
        public int? CantidadRestante { get; set; }
        public decimal? Costo { get; set; }
        public decimal? Valor { get; set; }
        public int? NumeroProceso { get; set; }
        public int? EstadoId { get; set; }
        public int? LineNumRequisicion { get; set; }
        public string? UserId { get; set; }
        public DateTime? Created { get; set; }
        public int? ObjectType { get; set; }
    }
}
