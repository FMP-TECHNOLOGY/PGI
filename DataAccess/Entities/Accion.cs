using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Accion
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? NombreCorto { get; set; } 

    public string? Descripcion { get; set; } 

    public string? Badge { get; set; } 

    public string? Subject { get; set; } 

    public string? Template { get; set; } 

    public string? EstadoId { get; set; } 

    public bool RequiereJustificacion { get; set; }

    public bool ValidarUsuario { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
