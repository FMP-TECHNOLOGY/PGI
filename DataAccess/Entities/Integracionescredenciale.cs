using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Integracionescredenciale
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int IntegracionId { get; set; }

    public int CredencialId { get; set; }

    public string? Url { get; set; } 

    public int Userid { get; set; }

    public DateTime Created { get; set; }

    public DateTime? UltimaEjecucion { get; set; }
}
