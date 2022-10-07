using currus.Models;
using Newtonsoft.Json;

namespace currus.Repository;

public class DriverFileRepository : FileRepository<Driver>, IDriverFileRepository
{
    private IEnumerable<Driver> _sortQuery;

    public DriverFileRepository() : base("drivers.json")
    {
        _sortQuery = from driver in _inMemoryStore
                     orderby driver.Id ascending
                     select driver;
    }

    public User? Get(Func<Driver, bool> predicate)
    {
        return _inMemoryStore.FirstOrDefault(predicate);
    }

    public IEnumerable<Driver> GetAll()
    {
        _sortQuery = from driver in _inMemoryStore
                     orderby driver.Id ascending
                     select driver;
        return _sortQuery;
    }

    public void Save()
    {
        _sortQuery = from driver in _inMemoryStore
                     orderby driver.Id ascending
                     select driver;
        File.WriteAllText(_fileName, JsonConvert.SerializeObject(_sortQuery.ToList()));
    }
}