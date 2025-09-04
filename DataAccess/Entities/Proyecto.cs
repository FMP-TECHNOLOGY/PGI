using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Proyecto
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int PoaId { get; set; }

    public int AreaId { get; set; }

    public string? Codigo { get; set; } 

    public string? Descripcion { get; set; } 

    public int ObjetivoId { get; set; }

    public string? Responsable { get; set; } 

    public int UnidadMedidaId { get; set; }

    public int PeriodicidadId { get; set; }

    public decimal LineaBase { get; set; }

    public decimal Meta { get; set; }

    public decimal Peso { get; set; }

    public bool? Aprobado { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public string? Dimension1 { get; set; } 

    public string? Dimension2 { get; set; } 

    public string? Dimension3 { get; set; } 
}
