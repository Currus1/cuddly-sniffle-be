using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace currus.Models;

public class User : IdentityUser
{
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public DateTime Birthdate { get; set; }
    [EmailAddress]
    [MaxLength(256)]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,4}|[0-9]{1,3})$")]
    public override string Email { get; set; } = "";
    [Phone]
    [RegularExpression(@"^((86|\+3706)\d{7})$")]
    public override string PhoneNumber { get; set; } = "";

    [JsonIgnore]
    public string? VehicleType { get; set; }

    [JsonIgnore]
    [RegularExpression(@"^[A-Z]{3}\d{3}$")]
    public string? LicenseNumber { get; set; }

    public string? DriversLicense { get; set; }
    [JsonIgnore]
    public virtual ICollection<Trip>? Trips { get; set; }

    public User()
    {
    }
}