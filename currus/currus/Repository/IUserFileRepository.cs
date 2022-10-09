using currus.Models;

namespace currus.Repository;

public interface IUserFileRepository : IFileRepository<User>
{
    public IEnumerable<User> SortedEnumerable();
}