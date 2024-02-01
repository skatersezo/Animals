using Animals.Core.Adaptors.Db;
using Animals.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Animals.Core.Adaptors.Repositories;

public class DogRepositoryAsync
{
    private readonly AnimalContext _uow;

    public DogRepositoryAsync(AnimalContext uow)
    {
        _uow = uow;
    }
    
    public async Task<IEnumerable<Dog>> GetByNameAsync(string name, CancellationToken ct = default)
    {
        return await _uow.Dogs.Where(d => string.Equals(d.Name, name)).ToListAsync(cancellationToken: ct);
    }
}