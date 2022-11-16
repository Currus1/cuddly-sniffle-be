using currus.Models;

namespace currus.Events
{
    public interface IStatusChanger<T> where T : class
    {
        //public delegate void StatusChangedEventHandler(object source, T args);
        //public event StatusChangedEventHandler? StatusChanged;
        //void ChangeEventHandler(Trip trip, bool add);
        void OnStatusChanged(object source, T args);
    }
}
