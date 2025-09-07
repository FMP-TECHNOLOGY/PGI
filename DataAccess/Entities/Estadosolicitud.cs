using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class EstadoSolicitud
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? Descripcion { get; set; } 

    public bool EstadoCompra { get; set; }

    public string? Color { get; set; } 

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
