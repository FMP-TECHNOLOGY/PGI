

using DataAccess.Entities.Base;

namespace DataAccess.Entities
{
    public class DecisionAprobadorEtapa : IIdentity
    {
        // Propiedades de la Entidad
        public string? Id { get; set; }
        public string EtapaId { get; set; }
        public string ApproverId { get; set; }
        public string Desicion { get; set; } // e.g., 'Aprobado', 'Rechazado', 'Pendiente'
        public string? Note { get; set; }

        // Propiedades del Objeto Base
        public int ObjectType { get; set; } = 83;
        public long LogInstance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
