using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace currus.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime Birthdate { get; set; }

    [RegularExpression(
        @"^([a-zA-Z0-9_\-\.]+)@(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,4}|[0-9]{1,3})$")]
    public string? Email { get; set; }

    [RegularExpression(@"^((86|\+3706)\d{7})$")]
    public string? PhoneNumber { get; set; }
    [JsonIgnore]
    public string? VehicleType { get; set; }
    [JsonIgnore]
    [RegularExpression(@"^[A-Z]{3}\d{3}$")]
    public string? LicenseNumber { get; set; }
    [JsonIgnore]
    private ICollection<Trip> _trips;
    [JsonIgnore]
    private ILazyLoader LazyLoader { get; set; }
    [JsonIgnore]
    public ICollection<Trip> Trips
    {
        get => LazyLoader.Load(this, ref _trips);
        set => _trips = value;
    }

    public User()
    {
    }

    private User(ILazyLoader lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    public User(int id, string? name, string? surname, DateTime birthdate, string? email,
        string? phoneNumber, string? vehicleType, string? licenseNumber)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Birthdate = birthdate;
        Email = email;
        PhoneNumber = phoneNumber;
        VehicleType = vehicleType;
        LicenseNumber = licenseNumber;
        if (Trips == null)
        {
            Trips = new List<Trip>();
        }
    }
}