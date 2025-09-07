using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class EjesEstrategico
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? PeiId { get; set; } 

    public string? Descripcion { get; set; } 

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
