using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Fondo
{
    public string? Id { get; set; } 

    public string? Descripcion { get; set; } 

    public string? SucursalId { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? UserId { get; set; } 

    public int? ObjectType { get; set; }
    public Boolean? Active { get; set; }
    public DateTime? Created { get; set; }
}
