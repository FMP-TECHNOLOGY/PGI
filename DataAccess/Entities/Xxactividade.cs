using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Xxactividade
{
    public string? Id { get; set; }
    public string? CompaniaId { get; set; }

    public string?Producto { get; set; }

    public string?Numero { get; set; }

    public string?Actividad { get; set; }

    public string?Peso { get; set; }

    public string?Tipo { get; set; }

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

    public string?PlanificadoDiciembre { get; set; }

    public string?Proyectoid { get; set; }

    public string?ActividadId { get; set; }

    public int? ObjectType { get; }
}
