using Animals.Core.Adaptors.Rest;

namespace Animals.API.Views;

public class AnimalsView
{
    public List<AnimalView> AnimalViews { get; set; }
    public List<Link> Links { get; set; }
}