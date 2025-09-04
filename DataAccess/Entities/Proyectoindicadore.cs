using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Proyectoindicadore
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int ProyectoId { get; set; }

    public int IndicadorId { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
