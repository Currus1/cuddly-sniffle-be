using currus.Data;
using currus.Models;

namespace currus.Repository;

public class TripDbRepository : DbRepository<Trip>, ITripDbRepository
{
    public TripDbRepository(ApplicationDbContext context) : base(context)
    {
    }

}

