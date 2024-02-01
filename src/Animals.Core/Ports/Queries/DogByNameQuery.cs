using Animals.Core.Domain.Models;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries;

public class DogByNameQuery : IQuery<DogByNameQuery.Result>
{
    public string DogName { get; }
    
    public DogByNameQuery(string dogName)
    {
        DogName = dogName;
    }
    
    public sealed class Result
    {
        public Result(List<DogModel> dogModels)
        {
            DogModels = dogModels;
        }

        public List<DogModel> DogModels { get; }
    }
}