using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class OcurrenciaRiesgo
    {
        public string? Id { get; set; }
        public string? ProbabilidadOcurrenciaId { get; set; }
        public string? RiesgoId { get; set; }
    }
}
