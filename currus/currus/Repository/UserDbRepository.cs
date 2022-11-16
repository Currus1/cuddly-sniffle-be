using currus.Data;
using currus.Models;
using Microsoft.EntityFrameworkCore;

namespace currus.Repository;

public class UserDbRepository : DbRepository<User>, IUserDbRepository
{
    ApplicationDbContext _context;
    public UserDbRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void DeleteById(int id)
    {
        var user = _context.User.Find(id);
        if (user != null)
            _context.User.Remove(user);
    }

    public ICollection<Trip> GetAllTrips(int id)
    {
        var user = _context.User.Include("Trips").FirstOrDefault(user => id == user.Id);
        if (user != null)
        {
            if (user.Trips != null)
            {
                return user.Trips;
            }
        }
        return null;
    }

    public User SetRelation(int userId, int tripId)
    {
        var user = _context.User.Find(userId);
        var trip = _context.Trip.Find(tripId);
        if (user != null)
        {
            user = _context.User.Include("Trips").FirstOrDefault(user => userId == user.Id);
            if (user.Trips == null)
                user.Trips = new List<Trip>();

            var trip = _context.Trip.Find(tripId);
            if(trip != null)
                user.Trips.Add(trip);
        }
        return user;
    }
}
