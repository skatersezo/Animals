using Paramore.Brighter;

namespace Animals.Core.Ports.Commands;
public class AddPigeonCommand : Command
{
    public string Colour { get; }
    public int PigeonId { get; set; }
    public AddPigeonCommand(string colour) : base(Guid.NewGuid())
    {
        Colour = colour;
    }
}
