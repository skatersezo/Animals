using Animals.Core.Domain.Models;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries;

public class PigeonByIdQuery : IQuery<PigeonByIdQuery.Result>
{
    public int PigeonId { get; }
    public PigeonByIdQuery(int pigeonId)
    {
        PigeonId = pigeonId;
    }
    
    public sealed class Result
    {
        public Result(PigeonModel pigeonModel)
        {
            PigeonModel = pigeonModel;
        }

        public PigeonModel PigeonModel { get; }
    }
}