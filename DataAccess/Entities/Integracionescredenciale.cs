using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class IntegracionesCredenciale
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? IntegracionId { get; set; } 

    public string? CredencialId { get; set; } 

    public string? Url { get; set; } 

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public DateTime? UltimaEjecucion { get; set; }

    public int? ObjectType { get; }
}
