using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Area
{
    public string? Id { get; set; } 

    public int CompaniaId { get; set; }

    public string? Descripcion { get; set; } 

    public string? CodigoIntegracion { get; set; } 

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public string?IdProyecto { get; set; }

    public string?CodigoPadre { get; set; }

    public string?DepartamentoId { get; set; }

    public int? ObjectType { get; }
}
