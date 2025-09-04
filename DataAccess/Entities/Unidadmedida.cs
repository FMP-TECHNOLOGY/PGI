using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Unidadmedida
{
    public int Id { get; set; }

    public string? Nombre { get; set; } 

    public string? Descripcion { get; set; } 

    public bool Active { get; set; }

    public int Userid { get; set; }

    public DateTime Created { get; set; }
}
