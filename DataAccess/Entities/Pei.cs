using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public partial class Pei : IUserIdentity, IIdentity
{
    public string? Id { get; set; } 

    //public string? CompaniaId { get; set; } 

    public string? Descripcion { get; set; } 

    public int AnoInicial { get; set; }

    public int AnoFinal { get; set; }

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
    [NotMapped]
    public List<EjesEstrategico>  EjesEstrategicos { get; set; } = new List<EjesEstrategico>();
}
