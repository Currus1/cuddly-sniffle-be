using currus.Models;

namespace currus.Repository;

public class DriverFileRepository : FileRepository<Driver>, IDriverFileRepository
{
    public DriverFileRepository() : base("drivers.json")
    {
    }
}