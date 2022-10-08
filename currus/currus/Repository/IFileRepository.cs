namespace currus.Repository;

public interface IFileRepository<T> where T : class
{
    void Add(T entity);
    public IEnumerable<T> GetAll();
    public T? Get(Func<T, bool> predicate);
    void Delete(T entity);
    public void Save();
}