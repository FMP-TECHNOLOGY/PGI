using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Tiporiesgo
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public string? Descripcion { get; set; } 

    public string? Badge { get; set; } 

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
