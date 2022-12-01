using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace currus.Models;

public class User : IdentityUser
{
    public string Name { get; set; }    
    public string? Surname { get; set; }
    public DateTime Birthdate { get; set; }
    public override string? Email { get; set; }
    public override string? PhoneNumber { get; set; }

    [JsonIgnore]
    public string? VehicleType { get; set; }

    [JsonIgnore]
    [RegularExpression(@"^[A-Z]{3}\d{3}$")]
    public string? LicenseNumber { get; set; }

    [JsonIgnore]
    public virtual ICollection<Trip>? Trips { get; set; }

    public User()
    {
    }

    public User(string name, string? surname, DateTime birthdate, string? email,
        string? phoneNumber, string? vehicleType, string? licenseNumber)
    {
        Name = name;
        Surname = surname;
        Birthdate = birthdate;
        Email = email;
        PhoneNumber = phoneNumber;
        VehicleType = vehicleType;
        LicenseNumber = licenseNumber;
        Trips = new List<Trip>();
    }
}