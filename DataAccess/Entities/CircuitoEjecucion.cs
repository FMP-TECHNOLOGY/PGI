

using DataAccess.Entities.Base;

namespace DataAccess.Entities
{
    public class CircuitoEjecucion : IIdentity, ICompanyIdentity
    {
        // Propiedades de la Entidad
        public string? Id { get; set; }
        public string CircuitId { get; set; }
        public int ForObjectType { get; set; }
        public string DocumentId { get; set; }
        public string? CurrentStepId { get; set; }
        public bool Finished { get; set; } = false;

        // Propiedades del Objeto Base
        public int ObjectType { get; set; } = 81;
        public long LogInstance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CompaniaId { get; set; }
    }
}
