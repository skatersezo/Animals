namespace Animals.Core.Ports.Repositories;

public interface IRepositoryAsync<T> where T : IEntity
{
    Task<T> AddAsync(T newEntity, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
    Task DeleteAllAsync(CancellationToken ct = default);
    Task<T?> GetAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
    Task UpdateAsync(T updatedEntity, CancellationToken ct = default);
}