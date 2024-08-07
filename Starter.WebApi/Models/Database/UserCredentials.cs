using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Starter.WebApi.Models.Database;

public partial class UserCredentials
{
    [Key]
    public long Id { get; set; }

    [StringLength(255)]
    public string EmailAddress { get; set; } = null!;

    [StringLength(255)]
    public string? HashedPassword { get; set; }

    [StringLength(100)]
    public string? UserRole { get; set; }

    [InverseProperty("UserCredentials")]
    public virtual ICollection<UserProfile> UserProfile { get; set; } = new List<UserProfile>();
}
