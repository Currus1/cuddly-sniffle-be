using currus.Models;

namespace currus.Repository;

public interface ITripDbRepository: IDbRepository<Trip>
{
    void DeleteById(int id);
    IEnumerable<Trip> GetAllByStatus(string tripStatus);
}
