using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class GrupoParametro
{
    public string? Id { get; set; } 

    public string? Descripcion { get; set; } 

    public bool Active { get; set; }

    public int Userid { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
