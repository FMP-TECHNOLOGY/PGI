using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Actividade
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? ProyectoId { get; set; } 

    public string?Descripcion { get; set; }

    public string? TipoActividad { get; set; } 

    public decimal Peso { get; set; }

    public int Orden { get; set; }

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int ObjectType { get;  }
}
