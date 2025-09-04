using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Solicitudcompra
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int PoaId { get; set; }

    public string? Codigo { get; set; } 
}
