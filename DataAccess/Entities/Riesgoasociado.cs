using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Riesgoasociado
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int ProyectoId { get; set; }

    public int RiesgoId { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
