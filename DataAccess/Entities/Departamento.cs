using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Departamento : IUserIdentity, IIdentity, ICompanyIdentity
{
    public string? Id { get; set; } 

    public string? Descripcion { get; set; } 

    public string? SucursalId { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? UserId { get; set; } 

    public int? ObjectType { get;  }
    public DateTime? Created { get; set; }
}
