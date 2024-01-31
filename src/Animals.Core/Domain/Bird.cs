namespace Animals.Core.Domain;

public abstract class Bird : Animal
{
    public override string Classification => nameof(Bird);
}