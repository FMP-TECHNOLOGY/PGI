using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Periodicidad : IUserIdentity, IIdentity
{
    public string? Id { get; set; } 

    public string? Nombre { get; set; } 

    public int Valor { get; set; }

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    //public Guid? CompaniaId { get; set; }
    public int? ObjectType { get; }
}
