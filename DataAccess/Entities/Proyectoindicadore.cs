using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ProyectoIndicadore
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? ProyectoId { get; set; } 

    public string? IndicadorId { get; set; } 

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
