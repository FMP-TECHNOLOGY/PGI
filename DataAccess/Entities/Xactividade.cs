using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Xactividade
{
    public string? Id { get; set; }
    public Guid? CompaniaId { get; set; }

    public string?Area { get; set; }

    public string?Producto { get; set; }

    public string?Numero { get; set; }

    public string?Actividad { get; set; }

    public string?Peso { get; set; }

    public string?Tipo { get; set; }

    public string?PlanificadoEnero { get; set; }

    public string?PlanificadoFebrero { get; set; }

    public string?PlanificadoMarzo { get; set; }

    public string?PlanificadoAbril { get; set; }

    public string?PlanificadoMayo { get; set; }

    public string?PlanificadoJunio { get; set; }

    public string?PlanificadoJulio { get; set; }

    public string?PlanificadoAgosto { get; set; }

    public string?PlanificadoSeptiembre { get; set; }

    public string?PlanificadoOctubre { get; set; }

    public string?PlanificadoNoviembre { get; set; }

    public string?PlanificadoDiciembre { get; set; }

    public int? ObjectType { get; set; }
}
