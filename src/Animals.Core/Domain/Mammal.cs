namespace Animals.Core.Domain;

public abstract class Mammal : Animal
{
    public override string Classification => nameof(Mammal);
}