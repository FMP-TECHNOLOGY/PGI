using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Compania
{
    public string Id { get; set; } 

    public string? Descripcion { get; set; } 

    public string? Direccion { get; set; } 

    public string? Telefono { get; set; } 

    public string? Rnc { get; set; } 

    public TimeOnly? HoraInicialIntegracion { get; set; }

    public TimeOnly? HoraFinalIntegracion { get; set; }

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get;  }
}
