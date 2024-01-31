using Animals.Core.Domain;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries;

public class AnimalsQuery : IQuery<AnimalsQuery.Result>
{
    public sealed class Result
    {
        public Result(IEnumerable<AnimalByIdQuery.Result<Animal>> animals)
        {
            Animals = animals;
        }

        public IEnumerable<AnimalByIdQuery.Result<Animal>> Animals { get; }
    }
}