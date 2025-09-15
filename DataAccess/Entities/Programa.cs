using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Programa
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? Descripcion { get; set; } 

    public int? ObjectType { get; } 

    public string? UserId { get; set; }

    public DateTime Created { get; set; }

    public Boolean? Active { get; set; }
}
