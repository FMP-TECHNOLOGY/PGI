using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Parametro
{
    public int Id { get; set; }

    public int GrupoParametroId { get; set; }

    public string? ParametroId { get; set; } 

    public string? Descripcion { get; set; } 

    public bool Active { get; set; }

    public int Userid { get; set; }

    public DateTime Created { get; set; }

    public bool OfuscarValor { get; set; }
}
