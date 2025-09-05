using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class UserCompania
{
    public string? Id { get; set; } 

    public int CompaniaId { get; set; }

    public int ValidUserId { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
