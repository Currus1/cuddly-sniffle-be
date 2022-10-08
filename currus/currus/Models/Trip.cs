using System.Text;
using System;
using currus.Extensions;

namespace currus.Models;

public class Trip
{
    public int Id { get; set; }
    public int DriverId { get; set; }

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

    public Trip(int id, int driverId, int[] userIds, string startingPoint, string destination, int seats, int hours, int minutes, double estimatedTripPrice)
    {
        Id = id;
        DriverId = driverId;
        UserIds = userIds;
        StartingPoint = startingPoint;
        Destination = destination;
        Hours = hours;
        Minutes = minutes;
        Seats = seats;
        EstimatedTripPrice = estimatedTripPrice;
    }
}