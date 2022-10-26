using currus.Models;

namespace currus.Repository
{
    public interface IUserDbRepository: IDbRepository<User>
    {
        void DeleteById(int id);
        User SetRelation(int userId, int tripId);
        ICollection<Trip> GetAllTrips(int id);
    }
}
