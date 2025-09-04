using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Productointegracion
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public string? Codigo { get; set; } 

    public string? Descripcion { get; set; } 

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public string? CodigoCatalogo { get; set; }

    public string? CuentaObjetal { get; set; }

    public string? Itbis { get; set; }

    public string? InventoryUoMentry { get; set; }
}
