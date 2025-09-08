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

    public Guid? CompaniaId { get; set; }
    public int? ObjectType { get; set; }
}
