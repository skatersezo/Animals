using Animals.Core.Adaptors.Rest;

namespace Animals.API.Views;

public class PigeonView
{
    public string Colour { get; set; }
    public string Classification { get; set; }
    public string Species { get; set; }
    public string Sound { get; set; }
    public List<Link> Links { get; set; }
}