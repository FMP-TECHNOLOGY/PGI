using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Menu : IUserIdentity, IIdentity, ICompanyIdentity
{
    public string? Id { get; set; } 

    public string? Nombre { get; set; } 

    public string? Ruta { get; set; } 

    public bool EsModulo { get; set; }

    public int PadreId { get; set; }

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public string? CompaniaId { get; set; }
    public int? ObjectType { get; }
}
