
namespace DataAccess.Entities
{
    public class AprobadorEtapa
    {
        // Propiedades de la Entidad
        public string? Id { get; set; }
        public string EtapaId { get; set; }
        public string ApproverId { get; set; }
        public string Type { get; set; } // Tipo de aprobador (e.g., 'User', 'Role', 'Position')

        // Propiedades del Objeto Base
        public int ObjectType { get; set; } = 80;
        public bool Active { get; set; }
        public long LogInstance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
