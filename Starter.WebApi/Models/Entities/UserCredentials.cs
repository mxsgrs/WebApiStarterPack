using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Starter.WebApi.Models.Entities;

public partial class UserCredentials
{
    [Key]
    public long Id { get; set; }

    [StringLength(255)]
    public string EmailAddress { get; set; } = "";

    [StringLength(255)]
    public string HashedPassword { get; set; } = "";

    [StringLength(100)]
    public string UserRole { get; set; } = "";

    [InverseProperty("UserCredentials")]
    public virtual ICollection<UserProfile> UserProfile { get; set; } = [];
}
