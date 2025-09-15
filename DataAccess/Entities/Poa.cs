using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Poa
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public int? Ano { get; set; } 

    public string? Descripcion { get; set; } 

    public bool Planificacion { get; set; }

    public int PeriodoEvidencia { get; set; }

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
