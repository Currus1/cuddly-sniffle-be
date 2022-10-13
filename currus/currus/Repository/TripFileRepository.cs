using currus.Models;

namespace currus.Repository
{
    public class TripFileRepository : FileRepository<Trip>, ITripFileRepository
    {
        public TripFileRepository() : base("trips.json")
        {
        }
    }
}
