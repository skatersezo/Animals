using Animals.Core.Adaptors.Db;
using Animals.Core.Domain;
using Animals.Core.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Animals.Core.Adaptors.Repositories;

public class AnimalRepositoryAsync<T> : IRepositoryAsync<T> where T : Animal
{
    private readonly AnimalContext _uow;

    public AnimalRepositoryAsync(AnimalContext uow)
    {
        _uow = uow;
    }


    public async Task<T> AddAsync(T newEntity, CancellationToken ct = default)
    {
        var savedEntity = _uow.Add(newEntity);
        await _uow.SaveChangesAsync(ct);
        return savedEntity.Entity;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var cat = await _uow.Set<T>().SingleAsync(c => c.Id == id, ct);
        _uow.Remove(cat);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task DeleteAllAsync(CancellationToken ct = default)
    {
        _uow.Set<T>().RemoveRange(await _uow.Set<T>().ToListAsync(ct));
        await _uow.SaveChangesAsync(ct);
    }

    public async Task<T?> GetAsync(int id, CancellationToken ct = default)
    {
        return await _uow.Set<T>().SingleOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await _uow.Set<T>().ToListAsync(cancellationToken: ct);
    }

    public async Task UpdateAsync(T updatedEntity, CancellationToken ct = default)
    {
        await _uow.SaveChangesAsync(ct);
    }
}