namespace Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //CRUD
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(int id);
        Task Add(TEntity tentity);
        void Update(TEntity tentity);
        void Delete(TEntity tentity);
    }
}