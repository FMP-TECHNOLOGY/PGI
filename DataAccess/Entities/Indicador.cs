using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Indicador
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? Nombre { get; set; } 

    public string? Descripcion { get; set; } 

    public string? UnidadMedidaId { get; set; } 

    public decimal ValorActual { get; set; }

    public decimal Objetivo { get; set; }

    public string? PeriodicidadId { get; set; } 

    public string? MedioVerificacion { get; set; } 

    public string? AreaId { get; set; } 

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
