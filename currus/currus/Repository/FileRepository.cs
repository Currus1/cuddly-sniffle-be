using Newtonsoft.Json;


namespace currus.Repository;

public class FileRepository<T> : IFileRepository<T> where T : class
{
    protected readonly string _fileName;
    protected readonly List<T>? _inMemoryStore;

    public FileRepository(string fileName = "default.json")
    {
        _fileName = fileName;
        _inMemoryStore = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_fileName)) ?? new List<T>();
        if(_inMemoryStore == null)
        {
            throw new Exception();
        }
    }

    public void Add(T entity)
    {
        _inMemoryStore.Add(entity);
    }

    public void Delete(T entity)
    {
        _inMemoryStore.Remove(entity);
    }

    public T? Get(Func<T, bool> predicate)
    {
        return _inMemoryStore.FirstOrDefault(predicate);
    }

    public IEnumerable<T> GetAll()
    {
        return _inMemoryStore;
    }

    public void Save()
    {
        File.WriteAllText(_fileName, JsonConvert.SerializeObject(_inMemoryStore));
    }
}