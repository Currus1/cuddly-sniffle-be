using currus.Models;

namespace currus.Repository;

public interface IDriverFileRepository : IFileRepository<Driver>
{
    public IEnumerable<Driver> SortedEnumerable();
}