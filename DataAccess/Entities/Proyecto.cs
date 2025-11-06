using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public partial class Proyecto : IUserIdentity, IIdentity, ICompanyIdentity
{
    public string? Id { get; set; }

    public string? CompaniaId { get; set; }
    public string? ProgramaId { get; set; }
    public string? FondoId { get; set; }

    public string? PoaId { get; set; }

    public string? AreaId { get; set; }

    public string? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public string? ObjetivoId { get; set; } 
    //public List<Objetivo> Objetivos { get; set; } = new List<Objetivo>();
    public string? Responsable { get; set; }

    public string? UnidadMedidaId { get; set; }
    
    public string? PeriodicidadId { get; set; }

    public decimal LineaBase { get; set; }

    public decimal Meta { get; set; }

    public decimal Peso { get; set; }

    public bool? Aprobado { get; set; }

    public string? UserId { get; set; }

    public DateTime Created { get; set; }

    //producto
    public string? Dimension1 { get; set; }
    //actividad
    public string? Dimension2 { get; set; }
    //departamento gerencial
    public string? Dimension3 { get; set; }
    //objetal
    //public string? Dimension4 { get; set; } 
    //fondo
    public string? Dimension5 { get; set; }

    public int? ObjectType { get; }
    [NotMapped]
    public List<Indicador>? Indicadores { get; set; } = new List<Indicador>();
    [NotMapped]

    public List<Riesgo>? Riesgo { get; set; } = new List<Riesgo>();
    [NotMapped]
    public List<Area>? Areas { get; set; } = new List<Area>();
    [NotMapped]
    public List<Actividade>? actividades { get; set; } = new List<Actividade>();

}
