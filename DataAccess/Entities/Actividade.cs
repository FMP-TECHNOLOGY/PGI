using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Actividade
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int ProyectoId { get; set; }

    public string? Descripcion { get; set; }

    public string? TipoActividad { get; set; } 

    public decimal Peso { get; set; }

    public int Orden { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public virtual User User { get; set; } 
}
