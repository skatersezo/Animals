using Animals.API.Views;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Domain.Models;

namespace Animals.API.Builders.ViewBuilders;

public class PigeonViewBuilder : AbstractViewBuilder<PigeonModel, PigeonView>
{
    public PigeonViewBuilder(IAmALinkBuilder linkBuilder) : base(linkBuilder)
    {
    }

    public override PigeonView Build(PigeonModel model)
    {
        var links = new List<Link>()
        {
            _linkBuilder.Build(Rels.Self, HttpMethods.Get, RouteNames.GetPigeon, new { Id = model.Id }),
            _linkBuilder.Build(Rels.Parent, HttpMethods.Get, RouteNames.GetMammal, new { Id = model.Id }),
            _linkBuilder.Build(Rels.ListAll, HttpMethods.Get, RouteNames.GetPigeons),
            _linkBuilder.Build(Rels.Add, HttpMethods.Post, RouteNames.AddPigeon),
            _linkBuilder.Build(Rels.Edit, HttpMethods.Put, RouteNames.EditPigeon, new { Id = model.Id }),
            _linkBuilder.Build(Rels.Delete, HttpMethods.Delete, RouteNames.DeletePigeon, new { Id = model.Id })
        };

        return new PigeonView()
        {
            Colour = model.Colour,
            Classification = model.Classification,
            Species = model.Species,
            Sound = model.Sound,
            Links = links
        };
    }
}