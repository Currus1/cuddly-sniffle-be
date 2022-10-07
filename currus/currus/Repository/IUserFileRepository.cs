using currus.Models;

namespace currus.Repository;

public interface IUserFileRepository : IFileRepository<User>
{
    IEnumerable<User> GetAll();
    User? Get(Func<User, bool> predicate);
    void Save();
}