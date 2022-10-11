using System.Text.Json.Serialization;
using currus.Enums;
using currus.Extensions;
using Microsoft.OpenApi.Extensions;

namespace currus.Models;

public class Trip
{
    public int Id { get; set; }
    public int DriverId { get; set; }
    public Coordinates Coords { get; set; }
    private int[]? _userIds;

    public int[] UserIds
    {
        get => _userIds;

        set
        {
            if (value.IsMoreThanZero(Seats))
            {
                _userIds = value;
                return;
            }

            _userIds = null;
        }
    }

    public string StartingPoint { get; set; }
    public string Destination { get; set; }
    public int Seats { get; set; }
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public decimal Distance { get; set; }
    public string VehicleType { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public decimal EstimatedTripPrice { get; set; }

    public Trip(int seats, int id, int driverId, Coordinates coords, int[] userIds, string startingPoint,
        string destination, int hours, int minutes, decimal distance)
    {
        Coords = coords;
        Seats = seats;
        Id = id;
        DriverId = driverId;
        UserIds = userIds;
        StartingPoint = startingPoint;
        Destination = destination;
        Hours = hours;
        Minutes = minutes;
        EstimatedTripPrice = CalculateTripPrice(hours, minutes, distance);
    }

    public decimal CalculateTripPrice(int hours, int minutes, decimal distance, int basePrice = 2)
    {
        decimal fullPrice = basePrice; // Widening type conversion
        int fullTime = hours * 60 + minutes;
        decimal pricePerKm = 0.2M;
        decimal pricePerMin = 0.1M;
        fullPrice += fullTime * pricePerMin + distance * pricePerKm;
        return fullPrice;
    }

    public int CalculateBasePrice(Trip trip)
    {
        switch (trip.VehicleType)
        {
            case "Sedan":
                return 2;
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