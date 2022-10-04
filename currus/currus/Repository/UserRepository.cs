using currus.Models;

namespace currus.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository() : base("users.json")
    {
    }
}