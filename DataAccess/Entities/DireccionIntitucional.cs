using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class DireccionIntitucional
{
    public string? Id { get; set; } 

    public string? Descripcion { get; set; } 

    public string? Rnc { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? UserId { get; set; } 
    public string? Telefono { get; set; } 

    public int? ObjectType { get; }
    public bool? Active { get; set; }
}
