using currus.Models;

namespace currus.Repository;

public interface ITripDbRepository: IDbRepository<Trip>
{
    void DeleteById(int id);
    IEnumerable<Trip> GetAllByStatus(string tripStatus);
    ICollection<User> GetAllUsers(int id);
    Trip GetTripAsNotTracked(int id);
    IEnumerable<Trip> GetTripsForUser(string userId);
    public bool AddUserToTrip(User user, int id);
}
