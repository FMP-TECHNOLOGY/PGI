using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class SolicitudCompra
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? PoaId { get; set; } 

    public string? Codigo { get; set; } 

    public int? ObjectType { get; }
}
