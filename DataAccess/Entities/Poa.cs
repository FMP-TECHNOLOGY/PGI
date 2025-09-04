using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Poa
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int Ano { get; set; }

    public string? Descripcion { get; set; } 

    public bool Planificacion { get; set; }

    public int PeriodoEvidencia { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
