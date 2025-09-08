namespace DataAccess.Entities;

public partial class User
{
    public string? Id { get; set; } 

    public ulong Su { get; set; }

    public string? FullName { get; set; } 

    public string?NationalIdNumber { get; set; }

    public string? Username { get; set; } 

    public string?Address { get; set; }

    public string?PicturePath { get; set; }

    public string?Phone { get; set; }

    public string?Phone2 { get; set; }

    public ulong PhoneConfirmed { get; set; }

    public ulong Phone2Confirmed { get; set; }

    public string?Email { get; set; }

    public ulong EmailConfirmed { get; set; }

    public ulong NotificationsEnabled { get; set; }

    public string? PasswordHash { get; set; } 

    public ulong ResetPasswordNextLogin { get; set; }

    public ulong PasswordExpires { get; set; }

    public DateTime? PasswordExpirationDate { get; set; }

    public ulong LockoutEnabled { get; set; }

    public DateTime? LockoutDueDate { get; set; }

    public int ObjectType { get; set; }

    public int AccessFailedCount { get; set; }

    public ulong Active { get; set; }

    public long LogInstance { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string?CreatedBy { get; set; }

    public string?UpdatedBy { get; set; }

    public bool IsLocked()
    {
        throw new NotImplementedException();
    }
}
