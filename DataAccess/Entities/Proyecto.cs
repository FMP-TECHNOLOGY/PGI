using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Proyecto
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? PoaId { get; set; } 

    public string? AreaId { get; set; } 

    public string? Codigo { get; set; } 

    public string? Descripcion { get; set; } 

    public string? ObjetivoId { get; set; } 

    public string? Responsable { get; set; } 

    public string? UnidadMedidaId { get; set; } 

    public string? PeriodicidadId { get; set; } 

    public decimal LineaBase { get; set; }

    public decimal Meta { get; set; }

    public decimal Peso { get; set; }

    public bool? Aprobado { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public string? Dimension1 { get; set; } 

    public string? Dimension2 { get; set; } 

    public string? Dimension3 { get; set; } 

    public int? ObjectType { get; set; }
}
