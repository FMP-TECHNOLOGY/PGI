using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Parametrosvalor
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int ParametroId { get; set; }

    public string? Value { get; set; } 

    public bool Active { get; set; }

    public int Userid { get; set; }

    public DateTime Created { get; set; }
}
