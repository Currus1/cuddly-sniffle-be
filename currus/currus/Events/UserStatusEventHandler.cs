using currus.Models;

namespace currus.Events
{
    public static class UserStatusEventHandler
    {

        public static EventHandler<UserStatusEventArgs> UserStatusChanged { get; set; }

        public static void ChangeEventHandler(Trip trip, bool add)
        {
            if (trip != null && trip.Users != null)
            {
                if(add)
                {
                    
                    foreach (User user in trip.Users)
                    {
                        if (user != null)
                        {
                            UserStatusChanged += user.OnStatusChanged;
                        }
                    }
                }
                else
                {
                    foreach (User user in trip.Users)
                    {
                        if (user != null)
                        {
                            UserStatusChanged -= user.OnStatusChanged;
                        }
                    }
                }
            }
        }

        public static void OnUserStatusChanged(object source, UserStatusEventArgs args)
        {
            if (UserStatusChanged != null)
                UserStatusChanged(source, args);
        }
    }
}
