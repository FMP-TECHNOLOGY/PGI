using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ProyectoRiesgo
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? ProyectoId { get; set; } 

    public string? RiesgoId { get; set; } 

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
