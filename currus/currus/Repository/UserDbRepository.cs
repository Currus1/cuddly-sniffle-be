using currus.Data;
using currus.Models;

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
}
