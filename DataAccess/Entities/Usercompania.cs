

namespace DataAccess.Entities;

public partial class UserCompania
{
    public string? Id { get; set; } 

    public string CompaniaId { get; set; }

    public string? ValidUserId { get; set; }

    public bool Active { get; set; }

    public string? UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
    public User? User { get; set; }
    public Compania? Company { get; set; }
}
