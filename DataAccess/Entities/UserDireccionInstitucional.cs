
using DataAccess.Entities.Base;

namespace DataAccess.Entities;

public partial class UserDireccionInstitucional : IUserIdentity, IIdentity
{
    public string? Id { get; set; } 

    //public string? CompaniaId { get; set; }
    public string? DirecionInstitucionalId { get; set; }

    public string? ValidUserId { get; set; }

    public bool Active { get; set; }

    public string UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
    public User? User { get; set; }
    public DireccionIntitucional? DireccionIntitucional { get; set; }
}
