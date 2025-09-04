using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Integracionlog
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int IntegracionId { get; set; }

    public bool Existoso { get; set; }

    public string? Mensaje { get; set; }

    public DateTime Created { get; set; }
}
