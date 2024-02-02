using Animals.API.Views;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Domain.Models;

namespace Animals.API.Builders.ViewBuilders;

public class CatViewBuilder : AbstractViewBuilder<CatModel, CatView>
{
    public CatViewBuilder(IAmALinkBuilder linkBuilder) : base(linkBuilder)
    {
    }

    public override CatView Build(CatModel model)
    {
        var links = new List<Link>()
        {
            _linkBuilder.Build(Rels.Self, HttpMethods.Get, RouteNames.GetCat, new { Id = model.Id }),
            _linkBuilder.Build(Rels.Parent, HttpMethods.Get, RouteNames.GetMammal, new { Id = model.Id }),
            _linkBuilder.Build(Rels.ListAll, HttpMethods.Get, RouteNames.GetCats),
            _linkBuilder.Build(Rels.Add, HttpMethods.Post, RouteNames.AddCat),
            _linkBuilder.Build(Rels.Edit, HttpMethods.Put, RouteNames.EditCat, new { Id = model.Id }),
            _linkBuilder.Build(Rels.Delete, HttpMethods.Delete, RouteNames.DeleteCat, new { Id = model.Id })
        };

        return new CatView()
        {
            FavouriteToy = model.FavouriteToy,
            Classification = model.Classification,
            Species = model.Species,
            Sound = model.Sound,
            Links = links
        };
    }
}