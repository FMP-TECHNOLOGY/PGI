using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Pacc
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? PoaId { get; set; } 

    public string? ProyectoId { get; set; } 

    public string? ActividadId { get; set; } 

    //codigo producto o item
    public string? CodigoIntegracion { get; set; } 

    //viene del producto
    public string? CodigoCatalogo { get; set; } 

    //viene del producto
    public string? CuentaObjetalId { get; set; } 

    public string? Descripcion { get; set; } 

    public string? Grupo { get; set; } 

    public decimal CostoEstimado { get; set; }

    public bool PermitirCambioCosto { get; set; }

    public bool Planificado { get; set; }

    public bool? Aprobado { get; set; }

    public decimal Mes1 { get; set; }

    public decimal Mes2 { get; set; }

    public decimal Mes3 { get; set; }

    public decimal Mes4 { get; set; }

    public decimal Mes5 { get; set; }

    public decimal Mes6 { get; set; }

    public decimal Mes7 { get; set; }

    public decimal Mes8 { get; set; }

    public decimal Mes9 { get; set; }

    public decimal Mes10 { get; set; }

    public decimal Mes11 { get; set; }

    public decimal Mes12 { get; set; }

    public bool Active { get; set; }
    public DateTime Created { get; set; }

    public int? ObjectType { get; }
    public string?UserId { get; set; }
    public string? DireccionId { get; set; }
    public string? SucursalId { get; set; }
    public string? DepartamentoId { get; set; }
}
