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

    public ICollection<User> GetAllUsers(int id)
    {
        var trip = _context.Trip.Include("Users").FirstOrDefault(trip => id == trip.Id);
        if (trip != null)
        {
            if (trip.Users != null)
            {
                return trip.Users;
            }
        }
        return null;
    }

    public Trip SetRelation(int tripId, int userId)
    {
        var trip = _context.Trip.Find(tripId);
        var user = _context.User.Find(userId);

        if (trip != null)
        {
            if (trip.Users != null && user != null)
            {
                trip.Users.Add(user);
            }
            else if (user != null)
            {
                trip.Users = new List<User>() { user };
            }
        }
        return trip;
    }

    public Trip GetTripAsNotTracked(int id)
    {
        var trip = _context.Trip.Include("Users").AsNoTracking().FirstOrDefault(trip => id == trip.Id); //Where(trip => trip.Id == id);
        return trip;
    }
}