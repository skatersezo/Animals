namespace Animals.Core.Ports.Exceptions;

public class DogNotFoundException : Exception
{
    public DogNotFoundException()
    {
    }

    public DogNotFoundException(string message) : base(message)
    {
    }
}