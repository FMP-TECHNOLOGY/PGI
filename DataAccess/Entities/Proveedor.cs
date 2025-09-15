using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Proveedor
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? Rnc { get; set; } 

    public string? RazonSocial { get; set; } 

    public string? Direccion { get; set; } 

    public string? Telefono { get; set; } 

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public string?CodigoIntegracion { get; set; }

    public string?FederalTaxId { get; set; }

    public int? ObjectType { get; }
}
