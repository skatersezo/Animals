namespace Animals.Core.Domain;

public class Cat : Mammal
{
    public Cat()
    {
        FavouriteToy = string.Empty;
    }
    
    public Cat(string favouriteToy)
    {
        FavouriteToy = favouriteToy;
    }

    public string FavouriteToy { get; }
    public override string Sound { get; } = "meow meow";
}