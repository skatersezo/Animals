namespace Animals.Core.Domain;

public class Dog : Mammal
{
    public Dog()
    {
        Name = string.Empty;
    }
    
    public Dog(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public override string Sound { get; } = "woof woof";
}
