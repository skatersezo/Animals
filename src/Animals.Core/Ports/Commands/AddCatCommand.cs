using Paramore.Brighter;

namespace Animals.Core.Ports.Commands;

public class AddCatCommand : Command
{
    public string FavouriteToy { get; }
    public int CatId { get; set; }
    
    public AddCatCommand(string favouriteToy) : base(Guid.NewGuid())
    {
        FavouriteToy = favouriteToy;
    }
}