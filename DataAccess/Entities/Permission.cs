﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Permission
{
    public string Id { get; set; }
    //public string? CompaniaId { get; set; }

    public string? Description { get; set; } 

    public long LogInstance { get; set; }

    public bool Active { get; set; }

    public int? ObjectType { get; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string ? CreatedBy { get; set; }

    public string ? UpdatedBy { get; set; }
}
