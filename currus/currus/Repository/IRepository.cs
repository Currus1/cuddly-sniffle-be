namespace currus.Repository;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    IEnumerable<T> GetAll();
    T? Get(Func<T, bool> predicate);
    void Delete(T entity);
    void Save();
}