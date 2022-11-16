using currus.Models;

namespace currus.Events
{
    public class UserStatusChanger
    {
        public static event StatusChangedEventHandler<UserStatusChanger, UserStatusEventArgs>? StatusChanged;

        public void ChangeEventHandler(Trip trip, bool add)
        {
            if (trip != null)
            {
                foreach (User user in trip.Users)
                {
                    if (user != null)
                    {
                        if(add)
                        {
                            StatusChanged += user.OnStatusChanged;
                        }
                        else
                        {
                            StatusChanged -= user.OnStatusChanged;
                        }
                    }
                }
            }
        }

        public void OnStatusChanged(object source, UserStatusEventArgs args)
        {
            if (StatusChanged != null)
                StatusChanged(this, args);
        }
    }
}
