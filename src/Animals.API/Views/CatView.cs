using Animals.Core.Adaptors.Rest;

namespace Animals.API.Views;

public class CatView
{
    public string FavouriteToy { get; set; }
    public string Classification { get; set; }
    public string Species { get; set; }
    public string Sound { get; set; }
    public List<Link> Links { get; set; }
}