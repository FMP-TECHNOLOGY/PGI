using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Evidencia
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? ProyectoId { get; set; } 

    public string? ActividadId { get; set; } 

    public int Mes { get; set; }

    public decimal Planificado { get; set; }

    public decimal Ejecutado { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
