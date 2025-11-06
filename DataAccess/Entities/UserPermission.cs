using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class UserPermission : IUserIdentity, IIdentity
{
    public string? Id { get; set; }

    public string? UserId { get; set; }
    //public string? CompaniaId { get; set; }

    public string? PermissionId { get; set; } 

    public long LogInstance { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string?CreatedBy { get; set; }

    public string?UpdatedBy { get; set; }

    public int? ObjectType { get; }
    public required Permission Permission { get; set; }
    public required User User { get; set; }

}
