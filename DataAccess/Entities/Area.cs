using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Area
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public string? Nombre { get; set; } 

    public string? CodigoIntegracion { get; set; } 

    public bool Active { get; set; }

    public int Userid { get; set; }

    public DateTime Created { get; set; }

    public string? IdProyecto { get; set; }

    public string? CodigoPadre { get; set; }
}
