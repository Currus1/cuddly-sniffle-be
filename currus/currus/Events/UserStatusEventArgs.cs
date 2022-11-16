namespace currus.Events
{
    public class UserStatusEventArgs : EventArgs
    {
        public bool InTrip { get; set; }

        public UserStatusEventArgs(bool inTrip)
        {
            InTrip = inTrip;
        }
    }
}
