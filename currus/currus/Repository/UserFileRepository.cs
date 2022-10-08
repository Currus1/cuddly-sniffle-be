using currus.Models;
using Newtonsoft.Json;

namespace currus.Repository;

public class UserFileRepository : FileRepository<User>, IUserFileRepository
{
    private IEnumerable<User> _sortQuery;

    public UserFileRepository() : base("users.json")
    {
        _sortQuery = from user in _inMemoryStore
                     orderby user.Id ascending
                     select user;
    }

    public IEnumerable<User> Sort()
    {
        _sortQuery = from user in _inMemoryStore
                     orderby user.Id ascending
                     select user;
        return _sortQuery;
    }

    public IEnumerable<User> GetAll()
    {
        _sortQuery = from user in _inMemoryStore
                     orderby user.Id ascending
                     select user;
        return _sortQuery;
    }
}