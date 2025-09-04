using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Pacc
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int PoaId { get; set; }

    public int ProyectoId { get; set; }

    public int ActividadId { get; set; }

    public string? CodigoIntegracion { get; set; } 

    public string? CodigoCatalogo { get; set; } 

    public string? CuentaObjetal { get; set; } 

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

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
