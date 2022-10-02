using System.Text;
using System;

namespace currus.Models;

    public class TripModel
{
    public string StartingPoint { get; set; }

    public string EndingPoint { get; set; }

    public int Seats { get; set; } 

    public TimeOnly EstimatedTripTime { get; set; }

    public double EstimatedTripPrice { get; set; }

    public TripModel(string startingPoint, string endingPoint, string tripRoute, int seats, TimeOnly estimatedTripTime, double estimatedTripPrice)
    {
        StartingPoint = startingPoint;
        EndingPoint = endingPoint;
        Seats = seats;
        EstimatedTripTime = estimatedTripTime;
        EstimatedTripPrice = estimatedTripPrice;
    }
}

