using currus.Models;
using System.Reflection.Metadata.Ecma335;

namespace currus.Repository
{
    public class TripRepository
    {
        public static List<OngoingTrip> ongoingTrips { get; set; } = new List<OngoingTrip>();
        public static List<TripModel> Trips { get; set; } = new List<TripModel>();
    }
}
