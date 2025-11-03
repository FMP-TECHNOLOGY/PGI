using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ProductoIntegracion
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? Codigo { get; set; } 

    public string? Descripcion { get; set; } 

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public string?CodigoCatalogo { get; set; }

    public string?CuentaObjetal { get; set; }

    //public string?Itbis { get; set; }

    public string?InventoryUoMentry { get; set; }

    public int? ObjectType { get; }
}
