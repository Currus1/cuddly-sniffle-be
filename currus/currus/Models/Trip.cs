using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace currus.Models;

public class Trip
{
    [Key]
    public int Id { get; set; }
    public double SLatitude { get; set; } // S - start
    public double SLongitude { get; set; }
    public double DLatitude { get; set; } // D - destination
    public double DLongitude { get; set; }
    public string StartingPoint { get; set; } = "";
    public string Destination { get; set; } = "";
    public int Seats { get; set; }
    public int? Hours { get; set; }
    public int? Minutes { get; set; }
    public decimal? Distance { get; set; }
    public string DriverId { get; set; } = "";
    public string VehicleType { get; set; } = "";
    public decimal EstimatedTripPrice { get; set; }
    public string TripStatus { get; set; } = "";
    public DateTime TripDate { get; set; }
    [JsonIgnore]
    public virtual ICollection<User>? Users { get; set; }

    public Trip()
    {
    }

    public Trip(double sLatitude, double sLongitude, 
        double dLatitude, double dLongitude, 
        string startingPoint, string destination, 
        decimal estimatedTripPrice, int seats, DateTime tripDate)
    {
        SLatitude = sLatitude;
        SLongitude = sLongitude;
        DLatitude = dLatitude;
        DLongitude = dLongitude;
        StartingPoint = startingPoint;
        Destination = destination;
        EstimatedTripPrice = estimatedTripPrice;
        Seats = seats;
        TripDate = tripDate;
    }
}