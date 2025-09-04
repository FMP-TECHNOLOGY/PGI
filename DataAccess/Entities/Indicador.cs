using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Indicador
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public string? Nombre { get; set; } 

    public string? Descripcion { get; set; } 

    public int UnidadMedidaId { get; set; }

    public decimal ValorActual { get; set; }

    public decimal Objetivo { get; set; }

    public int PeriodicidadId { get; set; }

    public string? MedioVerificacion { get; set; } 

    public int AreaId { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
