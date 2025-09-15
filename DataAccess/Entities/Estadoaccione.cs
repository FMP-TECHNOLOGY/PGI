using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class EstadoAccione
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? EstadoId { get; set; } 

    public string? AccionId { get; set; } 

    public string? UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
