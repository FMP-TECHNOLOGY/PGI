using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public partial class DetalleSolicitudCompra
{
    public string? Id { get; set; } 

    public string? SolicitudId { get; set; } 

    public string? PaccId { get; set; } 

    public string?Especificaciones { get; set; }

    public string? CuentaObjetal { get; set; } 

    public int Cantidad { get; set; }
    public int CantidadPendiente { get; set; }

    public decimal Costo { get; set; }

    public decimal Valor { get; set; }

    public int CantidadRecibida { get; set; }

    public decimal? CostoRecepcion { get; set; }

    public string?ProveedorId { get; set; }

    public string?OrdenCompra { get; set; }

    public string?NumeroProceso { get; set; }

    public DateTime? FechaAdjudicacion { get; set; }

    public decimal? PorcentajeDescuento { get; set; }

    public string?UmbralCode { get; set; }

    public string?TipoImpuestoCode { get; set; }

    public string?Estadoid { get; set; }

    public int? LineNumSolicitud { get; set; }

    public int? LineNumOrden { get; set; }

    public int? DocEntryOrdenCompra { get; set; }

    public int? DocNumOrdenCompra { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get;  }
    [NotMapped]
    public List<DocumentosEvidencia> Files { get; set; } = new List<DocumentosEvidencia>();
}

public partial class DetalleOrdenCompra
{
    public string? Id { get; set; } 

    public string? OrdenCompraId { get; set; } 

    public string? PaccId { get; set; } 

    public string? CuentaObjetal { get; set; } 

    public int Cantidad { get; set; }

    public decimal Costo { get; set; }

    public decimal Valor { get; set; }

    //cabecera
    //public string?ProveedorId { get; set; }

    public string?NumeroProceso { get; set; }

    public DateTime? FechaAdjudicacion { get; set; }

    public decimal? PorcentajeDescuento { get; set; }

    public string?UmbralCode { get; set; }

    public string?TipoImpuestoCode { get; set; }

    public string?Estadoid { get; set; }

    public int? LineNumSolicitud { get; set; }

    public int? LineNumOrden { get; set; }

    public int? DocEntryOrdenCompra { get; set; }

    public int? DocNumOrdenCompra { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get;  }
    [NotMapped]
    public List<DocumentosEvidencia> Files { get; set; } = new List<DocumentosEvidencia>();
}
