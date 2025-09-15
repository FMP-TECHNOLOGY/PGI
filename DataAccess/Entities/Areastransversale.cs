using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class AreasTransversale
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? ProyectoId { get; set; } 

    public string? AreaId { get; set; } 

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get;  }
}
