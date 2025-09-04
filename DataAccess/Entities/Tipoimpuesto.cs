using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Tipoimpuesto
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public string? Code { get; set; } 

    public string? Name { get; set; } 

    public decimal Rate { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
