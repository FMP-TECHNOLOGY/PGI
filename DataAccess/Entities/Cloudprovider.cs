namespace DataAccess.Entities;

public partial class CloudProvider
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? ProviderType { get; set; } 

    public string? AccessKey { get; set; } 

    public string? SecretKey { get; set; } 

    public string?Region { get; set; }

    public string?ContainerName { get; set; }

    public sbyte? Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get;  }
}
