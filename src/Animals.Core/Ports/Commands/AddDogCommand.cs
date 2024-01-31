using Paramore.Brighter;

namespace Animals.Core.Ports.Commands;

public class AddDogCommand : Command
{
    public string Name { get; }
    public int DogId { get; set; }
    
    public AddDogCommand(string name) : base(Guid.NewGuid())
    {
        Name = name;
    }
}