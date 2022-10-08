using currus.Enums;
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

    public IEnumerable<Driver> Sort()
    {
        _sortQuery = from driver in _inMemoryStore
                     orderby driver.Id ascending
                     select driver;
        return _sortQuery;
    }

    public IEnumerable<Driver> GetAll()
    {
        _sortQuery = from driver in _inMemoryStore
                     orderby driver.Id ascending
                     select driver;
        return _sortQuery;
    }

    public Driver CheckVehicleType(Driver driver, string defaultVehicleType = "Sedan")
    {
        foreach (var type in VehicleTypes.GetValues<VehicleTypes>())
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