using currus.Models;

namespace currus.Events
{
    public delegate void StatusChangedEventHandler<T, U>(T source, U args);
}
