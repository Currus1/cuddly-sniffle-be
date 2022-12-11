using currus.Data;
using currus.Models;
using Microsoft.AspNetCore.Http;
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
        if(_context.Trip != null)
        {
            var trip = _context.Trip.Find(id);
            if (trip != null)
                _context.Trip.Remove(trip);
        }
    }

    public IEnumerable<Trip> GetAllByStatus(string tripStatus)
    {
        IEnumerable<Trip> tripQuery =
            from trip in _context.Trip
            where trip.TripStatus == tripStatus
            select trip;
        return tripQuery;
    }

    public IEnumerable<Trip> GetTripsForUser(string userId)
    {
        if(_context.Trip != null)
        {
            var userTrips = from trip in _context.Trip
                        .Include(t => t.Users)
                        where trip.Users != null && trip.Users.Any(u => u.Id == userId)
                        select trip; 
            return userTrips.ToList();
        }
        return new Trip[] { };
    }

    public ICollection<User> GetAllUsers(int id)
    {
        if(_context.Trip != null)
        {
            var trip = _context.Trip.Include("Users").FirstOrDefault(trip => id == trip.Id);
            if (trip != null)
            {
                if (trip.Users != null)
                {
                    return trip.Users;
                }
            }
        }
        return new User[] { };
    }
    
    public bool AddUserToTrip(User user, int id)
    {
        if(_context.Trip != null)
        {
            var trip = _context.Trip.Include("Users").FirstOrDefault(trip => id == trip.Id);
            if(trip != null && trip.Users != null)
            {
                trip.Users.Add(user);
                return true;
            }
            return false;
        }
        return false;
    }

    public Trip GetTripAsNotTracked(int id)
    {
        if(_context.Trip != null && _context.Trip.Any())
        {
            var trip = _context.Trip.Include("Users").AsNoTracking().FirstOrDefault(trip => id == trip.Id);
            if(trip == null)
            {
                return new Trip();
            }
            return trip;
        }
        return new Trip();
    }
}