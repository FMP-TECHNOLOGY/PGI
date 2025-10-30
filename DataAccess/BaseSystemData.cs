
namespace DataAccess
{
    public class BaseSystemData
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ObjectType { get; set; }
        public bool Active { get; set; }
        public long LogInstance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? CompaniaId { get; set; }
    }
}
