using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ProfitCenter
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? AreaId { get; set; } 

    public string? CenterCode { get; set; } 

    public string?InWhichDimension { get; set; }

    public string?Udescripcion { get; set; }

    public string?Uproducto { get; set; }

    public string?Uactividad { get; set; }

    public string?UareaFunc { get; set; }

    public string?Ucodarfun { get; set; }

    public int? ObjectType { get; }
}
