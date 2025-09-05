using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Integracionlog
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? IntegracionId { get; set; } 

    public bool Existoso { get; set; }

    public string?Mensaje { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
