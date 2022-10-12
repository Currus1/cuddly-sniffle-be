using System.ComponentModel.DataAnnotations;

namespace currus.Models;

public struct OngoingTrip
{
    public int Id { get; set; }
    public DateTime? TripStart { get; set; }
    public DateTime? TripEnd { get; set; }

    public double? Duration { get; set; }

    public OngoingTrip(int id, DateTime tripStart, DateTime tripEnd)
    {
        Id = id;
        TripStart = tripStart;
        TripEnd = tripEnd;
        Duration = (tripEnd - tripStart).TotalMinutes;
    }
}