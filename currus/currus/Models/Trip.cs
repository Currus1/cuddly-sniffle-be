using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace currus.Models;

public class Trip
{
    [Key]
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? StartingPoint { get; set; }
    public string? Destination { get; set; }
    public int Seats { get; set; }
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public decimal Distance { get; set; }
    public string? VehicleType { get; set; }
    public decimal EstimatedTripPrice { get; set; }
    public string TripStatus { get; set; }
    [JsonIgnore]
    public virtual ICollection<User>? Users { get; set; }

    public Trip()
    {
    }

    public Trip(int id, double latitude, double longitude, string startingPoint, string destination, 
        int seats, int hours, int minutes, decimal distance, string vehicleType, decimal estimatedTripPrice, string tripStatus)
    {
        Id = id;
        Latitude = latitude;
        Longitude = longitude;
        StartingPoint = startingPoint;
        Destination = destination;
        Seats = seats;
        Hours = hours;
        Minutes = minutes;
        Distance = distance;
        VehicleType = vehicleType;
        EstimatedTripPrice = estimatedTripPrice;
        TripStatus = tripStatus;
        Users = new List<User>();
    }

    public decimal CalculateTripPrice(int hours, int minutes, decimal distance, int basePrice = 2)
    {
        if (hours <= 0 && minutes <= 0) throw new ArgumentOutOfRangeException();
        if (distance <= 0) throw new ArgumentOutOfRangeException();
        const decimal pricePerKm = 0.2M;
        const decimal pricePerMinute = 0.1M;
        decimal fullPrice = basePrice; // Widening type conversion
        var fullTime = hours * 60 + minutes;
        fullPrice += fullTime * pricePerMinute + distance * pricePerKm;
        return fullPrice;
    }

    public int CalculateBasePrice(Trip trip)
    {
        switch (trip.VehicleType)
        {
            case "SUV":
                return 3;
            case "Van":
                return 3;
            case "EV":
                return 1;
            default:
                return 2;
        }
    }
}