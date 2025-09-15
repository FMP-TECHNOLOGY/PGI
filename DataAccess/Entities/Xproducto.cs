using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Xproducto
{
    public string? Id { get; set; } 

    public string?Area { get; set; }

    public string?Codigo { get; set; }

    public string?EjeEstrategico { get; set; }

    public string?ObjetivoEstrategico { get; set; }

    public string?Producto { get; set; }

    public string?Indicador { get; set; }

    public int? Meta { get; set; }

    public int? LineaBase { get; set; }

    public decimal? Peso { get; set; }

    public string?RiesgosAsociados { get; set; }

    public decimal? PlanificadoEnero { get; set; }

    public decimal? PlanificadoFebrero { get; set; }

    public decimal? PlanificadoMarzo { get; set; }

    public decimal? PlanificadoAbril { get; set; }

    public decimal? PlanificadoMayo { get; set; }

    public decimal? PlanificadoJunio { get; set; }

    public decimal? PlanificadoJulio { get; set; }

    public decimal? PlanificadoAgosto { get; set; }

    public decimal? PlanificadoSeptiembre { get; set; }

    public decimal? PlanificadoOctubre { get; set; }

    public decimal? PlanificadoNoviembre { get; set; }

    public decimal? PlanificadoDiciembre { get; set; }

    public string?AreaId { get; set; }

    public string?IndicadorId { get; set; }

    public string?ObjetivoId { get; set; }

    public string?ProyectoId { get; set; }

    public string?Actividad { get; set; }

    public string?Tipo { get; set; }

    public string?PesoActividad { get; set; }

    public int? ObjectType { get; }
    public string? CompaniaId { get; set; }
}
