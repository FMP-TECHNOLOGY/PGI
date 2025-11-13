

using DataAccess.Entities.Base;

namespace DataAccess.Entities
{
    public class CircuitoDefinicion : IIdentity, ICompanyIdentity
    {
        // Propiedades de la Entidad
        public string? Id { get; set; }
        public string Name { get; set; }
        public int ForObjectType { get; set; }
        public string Description { get; set; }

        // Propiedades del Objeto Base
        public int ObjectType { get; set; } = 78;
        public bool Active { get; set; }
        public long LogInstance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CompaniaId { get; set; }
    }
}
