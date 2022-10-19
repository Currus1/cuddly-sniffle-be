using currus.Data;
using currus.Models;
using Microsoft.EntityFrameworkCore;

namespace currus.Repository;

public class TripDbRepository : DbRepository<Trip>, ITripDbRepository
{
    private ApplicationDbContext _context;
    public TripDbRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void DeleteById(int id)
    {
        var trip = _context.Trip.Find(id);
        if (trip != null)
            _context.Trip.Remove(trip);
    }

}

