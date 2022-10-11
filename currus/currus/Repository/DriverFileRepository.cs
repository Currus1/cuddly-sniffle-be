using currus.Enums;
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

    public Driver CheckVehicleType(Driver driver, string defaultVehicleType = "Sedan")
    {
        foreach (var type in Enum.GetValues<VehicleTypes>())
        {
            if (driver.VehicleType == type.ToString())
            {
                return driver;
            }
        }
        driver.VehicleType = defaultVehicleType;
        return driver;
    }
}