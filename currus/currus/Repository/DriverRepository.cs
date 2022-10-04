using currus.Models;

namespace currus.Repository;

public class DriverRepository : Repository<Driver>, IDriverRepository
{
    public DriverRepository() : base("drivers.json")
    {
    }
}