using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Sucursal : IIdentity, ICompanyIdentity
{
    public string? Id { get; set; } 
    public string? DireccionId { get; set; } 

    public string? Nombre { get; set; } 

    public string?Rnc { get; set; }

    public string? Userid { get; set; } 

    public string? CompaniaId { get; set; } 

    public int? ObjectType { get; }
}
