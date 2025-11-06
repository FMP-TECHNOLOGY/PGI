using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Role : IIdentity
{
    public string Id { get; set; } 

    public string? Name { get; set; } 

    public string? Description { get; set; } 

    public bool Active { get; set; }

    public long LogInstance { get; set; }

    public int? ObjectType { get; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string?CreatedBy { get; set; }

    public string?UpdatedBy { get; set; }
    //public string? CompaniaId { get; set; }
}
