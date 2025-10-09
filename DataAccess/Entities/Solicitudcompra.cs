using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public partial class SolicitudCompra
{
    public string? Id { get; set; }

    public string? CompaniaId { get; set; }

    public string? PoaId { get; set; }

    public string? Codigo { get; set; }

    public int? ObjectType { get; }
    public string? DireccionInstitucionalId { get; set; }
    public DateTime? Fecha { get; set; }
    public DateTime? FechaRequerida { get; set; }
    public string? SucursalId { get; set; }
    public string? DepartamentoId { get; set; }
    public string? AreaId { get; set; }
    public string? ProyectoId { get; set; }
    public bool? CompraPlanificada { get; set; }
    public string? Comentario { get; set; }
    [NotMapped]
    public List<DetalleSolicitudCompra> DetalleSolicitudCompras { get; set; } = new List<DetalleSolicitudCompra>();
}
