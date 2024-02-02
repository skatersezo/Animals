using Animals.Core.Domain.Models;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries;

public class CatByIdQuery : IQuery<CatByIdQuery.Result>
{
    public int CatId { get; }
    public CatByIdQuery(int catId)
    {
        CatId = catId;
    }
    
    public sealed class Result
    {
        public Result(CatModel catModel)
        {
            CatModel = catModel;
        }

        public CatModel CatModel { get; }
    }
}