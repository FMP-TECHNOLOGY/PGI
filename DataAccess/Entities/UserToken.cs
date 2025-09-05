using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class UserToken
{
    public string Id { get; set; } 

    public string? UserId { get; set; } 

    public string? AccessToken { get; set; } 

    public string? Jti { get; set; } 

    public string? Hash { get; set; } 

    public string? Typ { get; set; }

    public string? Alg { get; set; }

    public DateTime Exp { get; set; }

    public string? Host { get; set; } 

    public long LogInstance { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string?CreatedBy { get; set; }

    public string?UpdatedBy { get; set; }

    public int? ObjectType { get; set; }
}
