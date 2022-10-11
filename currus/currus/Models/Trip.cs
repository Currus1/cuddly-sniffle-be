using System.Text.Json.Serialization;
using currus.Extensions;

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
    public double EstimatedTripPrice { get; set; }

    public Trip(int seats, int id, int driverId, Coordinates coords, int[] userIds, string startingPoint,
        string destination, int hours, int minutes, double estimatedTripPrice)
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
        EstimatedTripPrice = estimatedTripPrice;
    }
}