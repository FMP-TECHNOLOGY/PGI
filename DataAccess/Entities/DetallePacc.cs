using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class DetallePacc
    {
        public string? Id { get; set; }
        public string? PaccId { get; set; }
        public string? RequisicionId { get; set; }
        public int? LineNumPacc { get; set; }
        public int? LineNumRequisicion { get; set; }
        public string? CuentaObjetalId { get; set; }
        public int? Cantidad { get; set; }
        public string? UserId { get; set; }
        public DateTime? Created { get; set; }
        public int? ObjectType { get; set; }
    }
}
