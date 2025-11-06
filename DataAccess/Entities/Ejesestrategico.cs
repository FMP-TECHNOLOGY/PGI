using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public partial class EjesEstrategico : IUserIdentity, IIdentity
{
    public string? Id { get; set; } 

    //public string? CompaniaId { get; set; } 

    public string? PeiId { get; set; } 

    public string? Descripcion { get; set; } 

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public bool Active { get; set; }

    public string? UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }

    [NotMapped]

    public List<Objetivo> Objetivos { get; set; } = new List<Objetivo>();
}
