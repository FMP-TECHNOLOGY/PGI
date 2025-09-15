using System;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Entities;

public partial class UserDireccionInstitucional
{
    public string? Id { get; set; } 

    public string DirecionInstitucionalId { get; set; }

    public int ValidUserId { get; set; }

    public bool Active { get; set; }

    public string UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
    public User? User { get; set; }
    public DireccionIntitucional? DireccionIntitucional { get; set; }
}
