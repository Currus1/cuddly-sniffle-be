using Newtonsoft.Json;

namespace currus.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly string _fileName;
    private readonly List<T>? _inMemoryStore;

    public Repository(string fileName = "default.json")
    {
        _fileName = fileName;
        _inMemoryStore = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_fileName)) ?? new List<T>();
    }

    public void Add(T entity)
    {
        _inMemoryStore.Add(entity);
        _inMemoryStore.Sort();
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