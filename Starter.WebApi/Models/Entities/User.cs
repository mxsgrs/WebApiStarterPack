namespace Starter.WebApi.Models.Entities;

public class User
{
    [Key]
    public long Id { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
    public string EmailAddress { get; set; } = "";
    public string HashedPassword { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly Birthday { get; set; }
    public Gender Gender { get; set; }
    public Role Role { get; set; }

    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "The phone number must be between 10 and 15 digits and may include a leading +.")]
    public string Phone { get; set; } = "";

    [InverseProperty("User")]
    public virtual Address? Address { get; set; }
}
