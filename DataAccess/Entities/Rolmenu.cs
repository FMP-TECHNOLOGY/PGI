using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class RolMenu
{
    public string? Id { get; set; } 

    public string? MenuId { get; set; } 

    public string? RolId { get; set; } 

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
