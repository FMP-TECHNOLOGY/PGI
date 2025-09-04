using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Cuentaobjetal
{
    public string? Cuenta { get; set; } 

    public string? Descripcion { get; set; } 

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
