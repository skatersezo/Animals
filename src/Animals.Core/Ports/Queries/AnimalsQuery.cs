using Animals.Core.Domain;
using Animals.Core.Domain.Models;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries;

public class AnimalsQuery : IQuery<AnimalsQuery.Result>
{
    public sealed class Result
    {
        public List<AnimalModel> AnimalModels { get; }
        
        public Result(List<AnimalModel> animalModels)
        {
            AnimalModels = animalModels;
        }
    }
}