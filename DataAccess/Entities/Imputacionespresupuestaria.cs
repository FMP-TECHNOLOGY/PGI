using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ImputacionesPresupuestaria
{
    public string? Id { get; set; }
    public string CuentaObjetalId { get; set; }

    public string? Descripcion { get; set; } 

    public string? CuentaObjeto { get; set; } 

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
