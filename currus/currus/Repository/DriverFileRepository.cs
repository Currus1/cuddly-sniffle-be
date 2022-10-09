using currus.Models;
using Newtonsoft.Json;

namespace currus.Repository;

public class DriverFileRepository : FileRepository<Driver>, IDriverFileRepository
{
    public DriverFileRepository() : base("drivers.json")
    {

    }

    public IEnumerable<Driver> SortedEnumerable()
    {
        IEnumerable<Driver> _sortQuery = from driver in _inMemoryStore
                                         orderby driver.Id ascending
                                         select driver;
        return _sortQuery;
    }

    public IEnumerable<Driver> GetAll()
    {
        return SortedEnumerable();
    }
    public void Save()
    {
        File.WriteAllText(_fileName, JsonConvert.SerializeObject(SortedEnumerable().ToList()));
    }
}
}