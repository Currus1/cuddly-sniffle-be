namespace currus.Repository
{
    public interface IDbRepository<T> where T : class
    {
        Task Add(T entity);
        public IEnumerable<T> GetAll();
        public T? Get(Func<T, bool> predicate);
        void Delete(T entity);
        public void Save();
        public Task SaveAsync();
    }
}
