using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Starter.WebApi.Models.Database;

public partial class UserCredentials
{
    [Key]
    public long Id { get; set; }

    [StringLength(255)]
    public string EmailAddress { get; set; } = string.Empty;

    [StringLength(255)]
    public string HashedPassword { get; set; } = string.Empty;

    [StringLength(100)]
    public string UserRole { get; set; } = string.Empty;

    [InverseProperty("UserCredentials")]
    public virtual ICollection<UserProfile> UserProfile { get; set; } = [];
}
