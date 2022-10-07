using currus.Models;

namespace currus.Repository;

public interface IDriverFileRepository : IFileRepository<Driver>
{
    IEnumerable<Driver> GetAll();
    User? Get(Func<Driver, bool> predicate);
    void Save();
}