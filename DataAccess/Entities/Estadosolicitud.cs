using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Estadosolicitud
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public string? Descripcion { get; set; } 

    public bool EstadoCompra { get; set; }

    public string? Color { get; set; } 

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public virtual User User { get; set; } 
}
