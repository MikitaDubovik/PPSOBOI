namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int key);
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
