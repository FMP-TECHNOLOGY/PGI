using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Pei
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public string? Descripcion { get; set; } 

    public int AnoInicial { get; set; }

    public int AnoFinal { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
