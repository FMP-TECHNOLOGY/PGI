using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Ejesestrategico
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int PeiId { get; set; }

    public string? Descripcion { get; set; } 

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public virtual User User { get; set; } 
}
