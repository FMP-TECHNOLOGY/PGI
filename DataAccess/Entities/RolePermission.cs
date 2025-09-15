using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class RolePermission
{
    public string? Id { get; set; }

    public string RoleId { get; set; } 

    public string? PermissionId { get; set; } 

    public long LogInstance { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string?CreatedBy { get; set; }

    public string?UpdatedBy { get; set; }

    //public string? CompaniaId { get; set; }
    public int? ObjectType { get; }
    public required Role Role { get; set; }
    public required Permission Permission { get; set; }

}
