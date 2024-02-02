using Animals.Core.Domain.Models;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries;

public class DogByIdQuery : IQuery<DogByIdQuery.Result>
{
    public int DogId { get; }
    public DogByIdQuery(int dogId)
    {
        DogId = dogId;
    }
    
    public sealed class Result
    {
        public DogModel DogModel;

        public Result(DogModel dogModel)
        {
            DogModel = dogModel;
        }
    }
}