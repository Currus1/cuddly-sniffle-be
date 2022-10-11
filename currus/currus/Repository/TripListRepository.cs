using currus.Models;

namespace currus.Repository;

public class TripListRepository
{
    public static List<OngoingTrip> ongoingTrips { get; set; } = new();
    public static List<Trip> Trips { get; set; } = new();
}