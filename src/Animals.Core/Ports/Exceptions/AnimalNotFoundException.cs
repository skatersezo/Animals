namespace Animals.Core.Ports.Exceptions;

public class AnimalNotFoundException : Exception
{
    public AnimalNotFoundException()
    {
    }

    public AnimalNotFoundException(string message) : base(message)
    {
        
    }
}