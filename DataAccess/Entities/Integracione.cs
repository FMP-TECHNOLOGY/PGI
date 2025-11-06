using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Integracione : IUserIdentity, IIdentity
{
    public string? Id { get; set; } 

    public string? Descripcion { get; set; } 

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public Guid? CompaniaId { get; set; }
    public int? ObjectType { get; }
}
