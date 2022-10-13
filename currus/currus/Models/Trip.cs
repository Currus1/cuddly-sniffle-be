using currus.Extensions;

namespace currus.Models;

public class Trip : IComparable
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
            if (value.IsLengthLessThanOrEqualTo(Seats))
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
    public decimal EstimatedTripPrice { get; set; }

    public Trip()
    {
    }

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
        Distance = distance;
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

    public int CompareTo(object? obj) // Sorting users by their name alhapebtically
    {
        var other = (Trip)obj; // narrowing type conversion
        if (string.Compare(Destination, other.Destination, StringComparison.Ordinal) < 0)
            return -1;
        if (string.Compare(Destination, other.Destination, StringComparison.Ordinal) > 0)
            return 1;
        return 0;
    }
}