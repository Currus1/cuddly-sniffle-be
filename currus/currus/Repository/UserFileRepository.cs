using currus.Models;

namespace currus.Repository;

public class UserFileRepository : FileRepository<User>, IUserFileRepository
{
    public UserFileRepository() : base("users.json")
    {
    }
}