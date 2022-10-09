using currus.Models;
using Newtonsoft.Json;

namespace currus.Repository;

public class UserFileRepository : FileRepository<User>, IUserFileRepository
{

    public UserFileRepository() : base("users.json")
    {

    }

    public IEnumerable<User> SortedEnumerable()
    {
        IEnumerable<User> _sortQuery = from user in _inMemoryStore
                                       orderby user.Id ascending
                                       select user;
        return _sortQuery;
    }

    public IEnumerable<User> GetAll()
    {
        return SortedEnumerable();
    }
    public void Save()
    {
        File.WriteAllText(_fileName, JsonConvert.SerializeObject(SortedEnumerable().ToList()));
    }
}