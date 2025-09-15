using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public partial class User : EqualityComparer<User>
{
    public string Id { get; set; }

    public bool Su { get; set; }
    //public string? CompaniaId { get; set; }

    public string? FullName { get; set; }

    public string? NationalIdNumber { get; set; }

    public string? Username { get; set; }

    public string? Address { get; set; }

    public string? PicturePath { get; set; }

    public string? Phone { get; set; }

    public string? Phone2 { get; set; }

    public ulong PhoneConfirmed { get; set; }

    public ulong Phone2Confirmed { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool NotificationsEnabled { get; set; }
    [Required(AllowEmptyStrings = false), JsonIgnore]

    public string? PasswordHash { get; set; } = string.Empty;

    public bool ResetPasswordNextLogin { get; set; }

    public bool PasswordExpires { get; set; }

    public DateTime? PasswordExpirationDate { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTime? LockoutDueDate { get; set; }

    public int ObjectType { get;  }

    public int AccessFailedCount { get; set; } = 0;

    public bool Active { get; set; }

    public long LogInstance { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
    public string? CompaniaId { get; set; }
    //[NotMapped]
    public Compania? Company { get; set; }
    //[NotMapped]

    public List<Compania> Companies { get; set; } = new();
    //[NotMapped]

    public List<Role> Roles { get; set; } = new();
    //[NotMapped]

    public List<Permission> Permissions { get; set; } = new();

    public override bool Equals(object? obj)
    {
        if (obj is User user)
            return Equals(this, user);

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return GetHashCode(this);
    }

    public override bool Equals(User? x, User? y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (x is null && y is null)
            return true;

        if (x is null || y is null)
            return false;

        if (x.LogInstance == y.LogInstance && x.Id == y.Id)
            return true;

        return false;

    }

    public override int GetHashCode([DisallowNull] User obj)
    {
        return LogInstance.GetHashCode() ^ Id.GetHashCode();
    }

    public bool IsLocked()
        => LockoutEnabled && LockoutDueDate is not null;

    public void ResetLockout()
    {
        LockoutEnabled = false;
        LockoutDueDate = null;
        AccessFailedCount = new int();
    }


}
