using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class UserSucursal : IUserIdentity, IIdentity
{
    public string? Id { get; set; } 

    public string? SucursalId { get; set; }
    //public string? DirecccionId { get; set; }

    public string? ValidUserId { get; set; }

    public bool Active { get; set; }

    public string UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
    public User? User { get; set; }
    public Sucursal? Sucursal { get; set; }
}
