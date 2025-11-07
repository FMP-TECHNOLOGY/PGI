using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public partial class Compania : IUserIdentity, IIdentity
{
    public string? Id { get; set; } 

    public string? Descripcion { get; set; } 

    public string? Direccion { get; set; } 

    public string? Telefono { get; set; } 

    public string? Rnc { get; set; } 

    public DateTime? HoraInicialIntegracion { get; set; }

    public DateTime? HoraFinalIntegracion { get; set; }

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    //public int? ObjectType { get;  }

    [NotMapped]
    public List<DireccionIntitucional> Direcciones { get; set; } = new();
}
