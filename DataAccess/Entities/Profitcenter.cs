using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Profitcenter
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int AreaId { get; set; }

    public string? CenterCode { get; set; } 

    public string? InWhichDimension { get; set; }

    public string? Udescripcion { get; set; }

    public string? Uproducto { get; set; }

    public string? Uactividad { get; set; }

    public string? UareaFunc { get; set; }

    public string? Ucodarfun { get; set; }
}
