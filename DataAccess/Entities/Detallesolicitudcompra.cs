using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Detallesolicitudcompra
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }

    public int PaccId { get; set; }

    public string? Especificaciones { get; set; }

    public string? CuentaObjetal { get; set; } 

    public int Cantidad { get; set; }

    public decimal Costo { get; set; }

    public decimal Valor { get; set; }

    public int CantidadRecibida { get; set; }

    public decimal? CostoRecepcion { get; set; }

    public string? ProveedorId { get; set; }

    public string? OrdenCompra { get; set; }

    public string? NumeroProceso { get; set; }

    public DateTime? FechaAdjudicacion { get; set; }

    public decimal? PorcentajeDescuento { get; set; }

    public string? UmbralCode { get; set; }

    public string? TipoImpuestoCode { get; set; }

    public int? Estadoid { get; set; }

    public int? LineNumSolicitud { get; set; }

    public int? LineNumOrden { get; set; }

    public int? DocEntryOrdenCompra { get; set; }

    public int? DocNumOrdenCompra { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public virtual User User { get; set; } 
}
