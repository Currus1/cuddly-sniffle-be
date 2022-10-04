using currus.Models;

namespace currus.Repository;

public class TripRepository
{
    public static List<OngoingTrip> ongoingTrips { get; set; } = new();
    public static List<Trip> Trips { get; set; } = new();
}