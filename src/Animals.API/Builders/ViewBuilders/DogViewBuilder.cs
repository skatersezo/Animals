using Animals.API.Views;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Domain.Models;

namespace Animals.API.Builders.ViewBuilders;

public class DogViewBuilder : AbstractViewBuilder<DogModel, DogView>
{
    public DogViewBuilder(IAmALinkBuilder linkBuilder) : base(linkBuilder)
    {
    }

    public override DogView Build(DogModel model)
    {
        var links = new List<Link>()
        {
            _linkBuilder.Build(Rels.Self, HttpMethods.Get, RouteNames.GetDog, new { Id = model.Id }),
            _linkBuilder.Build(Rels.Parent, HttpMethods.Get, RouteNames.GetMammal, new { Id = model.Id }),
            _linkBuilder.Build(Rels.ListAll, HttpMethods.Get, RouteNames.GetDogs),
            _linkBuilder.Build(Rels.Add, HttpMethods.Post, RouteNames.AddDog),
            _linkBuilder.Build(Rels.Edit, HttpMethods.Put, RouteNames.EditDog, new { Id = model.Id }),
            _linkBuilder.Build(Rels.Delete, HttpMethods.Delete, RouteNames.DeleteDog, new { Id = model.Id })
        };

        return new DogView()
        {
            Name = model.Name,
            Classification = model.Classification,
            Species = model.Species,
            Sound = model.Sound,
            Links = links
        };
    }
}