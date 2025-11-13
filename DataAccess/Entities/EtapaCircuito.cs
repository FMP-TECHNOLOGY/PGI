
using DataAccess.Entities.Base;

namespace DataAccess.Entities
{
    public class EtapaCircuito : IIdentity
    {
        // Propiedades de la Entidad
        public string? Id { get; set; }
        public string CircuitId { get; set; }
        public string Name { get; set; }
        public int MinApprovals { get; set; }
        public int MinRejects { get; set; }
        public string? RejectStepId { get; set; }
        public int Orden { get; set; }

        // Propiedades del Objeto Base
        public int ObjectType { get; set; } = 79;
        public bool Active { get; set; }
        public long LogInstance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string ?UpdatedBy { get; set; }
    }
}
