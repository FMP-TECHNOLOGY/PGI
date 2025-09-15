using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Umbrale
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? Code { get; set; } 

    public string? Name { get; set; } 

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
