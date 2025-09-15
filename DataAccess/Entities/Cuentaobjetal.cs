using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class CuentaObjetal
{
    public string? Id { get; set; } 
    //public string? Cuenta { get; set; } 

    public string? Descripcion { get; set; } 

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get;  }
}
