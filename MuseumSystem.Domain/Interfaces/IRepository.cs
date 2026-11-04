namespace MuseumSystem.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
}