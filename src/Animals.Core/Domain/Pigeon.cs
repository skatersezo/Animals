namespace Animals.Core.Domain;

public class Pigeon : Bird
{
    public Pigeon()
    {
        Colour = string.Empty;
    }
    
    public Pigeon(string colour)
    {
        Colour = colour;
    }

    public string Colour { get; }
    public override string Sound { get; } = "coo coo";
}