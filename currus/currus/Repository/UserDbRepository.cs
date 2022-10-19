using currus.Data;
using currus.Models;

namespace currus.Repository
{
    public class UserDbRepository : DbRepository<User>, IUserDbRepository
    {
        public UserDbRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
