using currus.Models;

namespace currus.Repository;

public interface IDriverFileRepository : IFileRepository<Driver>
{
    public IEnumerable<Driver> SortedEnumerable();
    public Driver CheckVehicleType(Driver driver, string vehicleType = "Sedan");
}