namespace currus.Repository
{
    public interface IDbRepository<T> where T : class
    {
        public Task Add(T entity);
        public IEnumerable<T> GetAll();
        public T? Get(int id);
        void Delete(T entity);
        public void Save();
        public Task SaveAsync();
        public void Update(T entity);
    }
}
