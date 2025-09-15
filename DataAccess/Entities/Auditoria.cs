using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Auditoria
{
    public string? Id { get; set; } 

    public string? ClavePrimaria { get; set; } 

    public string? Tabla { get; set; } 

    public string? Campo { get; set; } 

    public string? ValorAnterior { get; set; } 

    public string? ValorActual { get; set; } 

    public string? Host { get; set; } 

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get;  }
}
