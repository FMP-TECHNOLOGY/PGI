
using DataAccess.Entities.Base;

namespace DataAccess.Entities
{
    public class EtapaCircuitoEjecucion : IIdentity
    {
        // Propiedades de la Entidad
        public string? Id { get; set; }
        public string ExecutionId { get; set; }
        public int Approvals { get; set; }
        public int Rejects { get; set; }
        public bool Finished { get; set; } = false;

        // Propiedades del Objeto Base
        public int ObjectType { get; set; } = 82;
        public long LogInstance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
