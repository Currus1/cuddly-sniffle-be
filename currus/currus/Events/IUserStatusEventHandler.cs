using currus.Models;

namespace currus.Events
{
    public interface IUserStatusEventHandler
    {
        //EventHandler<UserStatusEventArgs> UserStatusChanged { get; set; }
        void ChangeEventHandler(Trip trip, bool add);
        void OnUserStatusChanged(UserStatusEventArgs args);
    }
}
