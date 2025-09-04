using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; } 

    public string? Claim { get; set; } 

    public string? ClaimValue { get; set; } 

    public bool Active { get; set; }

    public int Userid { get; set; }

    public DateTime Created { get; set; }
}
