using Animals.Core.Domain;
using Animals.Core.Domain.Models;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries;

public class AnimalByIdQuery : IQuery<AnimalByIdQuery.Result<Animal>>
{
    public AnimalByIdQuery(int animalId)
    {
        AnimalId = animalId;
    }

    public int AnimalId { get; }

    
    public sealed class Result<T> where T : Animal
    {
        public AnimalModel AnimalModel;
        
        public Result(T animal)
        {
            AnimalModel = new AnimalModel
            {
                Id = animal.Id,
                Classification = animal.Classification,
                Species = animal.Species,
                Sound = animal.Sound
            };
        }
    }
}