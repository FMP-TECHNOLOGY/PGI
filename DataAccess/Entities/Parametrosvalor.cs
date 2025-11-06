using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ParametrosValor : IUserIdentity, IIdentity, ICompanyIdentity
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? ParametroId { get; set; } 

    public string? Value { get; set; } 

    public bool Active { get; set; }

    public string? UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
