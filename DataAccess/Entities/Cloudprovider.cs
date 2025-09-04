using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Cloudprovider
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public string? ProviderType { get; set; } 

    public string? AccessKey { get; set; } 

    public string? SecretKey { get; set; } 

    public string? Region { get; set; }

    public string? ContainerName { get; set; }

    public bool? Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
