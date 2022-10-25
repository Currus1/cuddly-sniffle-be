using currus.Data;
using currus.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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

    public IEnumerable<Trip> GetAllByStatus(string tripStatus)
    {
        IEnumerable<Trip> tripQuery =
            from trip in _context.Trip
            where trip.TripStatus == tripStatus
            select trip;
        return tripQuery;
    }
}

