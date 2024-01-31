using Animals.Core.Domain;
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
        public string Classification { get; }
        public string Species { get; }
        public string Sound { get; }
        
        public Result(T animal)
        {
            Classification = animal.Classification;
            Species = animal.Species;
            Sound = animal.Sound;
        }
    }
}