namespace currus.Models;

public class Trip
{
    public int Id { get; set; }
    public int DriverId { get; set; }
    public int UserId { get; set; }

    public string StartingPoint { get; set; }

    public string Destination { get; set; }

    public int Seats { get; set; }

    public int Hours { get; set; }
    public int Minutes { get; set; }

    public double EstimatedTripPrice { get; set; }

    public Trip(int id, int driverId, int userId, string startingPoint, string destination, int seats, int hours,
        int minutes, double estimatedTripPrice)
    {
        Id = id;
        DriverId = driverId;
        UserId = userId;
        StartingPoint = startingPoint;
        Destination = destination;
        Seats = seats;
        Hours = hours;
        Minutes = minutes;
        EstimatedTripPrice = estimatedTripPrice;
    }
}