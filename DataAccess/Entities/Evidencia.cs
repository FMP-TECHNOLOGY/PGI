using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Evidencia
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int ProyectoId { get; set; }

    public int ActividadId { get; set; }

    public int Mes { get; set; }

    public decimal Planificado { get; set; }

    public decimal Ejecutado { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public virtual User User { get; set; } 
}
