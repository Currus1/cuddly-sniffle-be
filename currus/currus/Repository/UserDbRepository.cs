﻿using currus.Data;
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
        var user = _context.User?.Find(userId);
        if (user != null)
        {
            if (user.Trips == null)
                user.Trips = new List<Trip>();
            user.Trips.Add(new Trip { Id = tripId });
            return user;
        }
        return user;
    }
}
