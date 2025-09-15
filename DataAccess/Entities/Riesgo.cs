using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Riesgo
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? Nombre { get; set; } 

    public string? Descripcion { get; set; } 

    public int TipoRiesgoId { get; set; }

    public string? ProbabilidadOcurrencia { get; set; } 

    public string? Impacto { get; set; } 

    public string? Mitigacion { get; set; } 

    public string? Origen { get; set; } 

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
