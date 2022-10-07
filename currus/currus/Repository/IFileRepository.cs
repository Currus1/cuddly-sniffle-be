namespace currus.Repository;

public interface IFileRepository<T> where T : class
{
    void Add(T entity);
    void Delete(T entity);
}