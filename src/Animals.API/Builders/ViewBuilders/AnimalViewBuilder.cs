using Animals.API.Views;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Domain.Models;

namespace Animals.API.Builders.ViewBuilders;

public class AnimalViewBuilder : AbstractViewBuilder<AnimalModel, AnimalView>
{
    public AnimalViewBuilder(IAmALinkBuilder linkBuilder) : base(linkBuilder)
    {
    }

    public override AnimalView Build(AnimalModel model)
    {
        var links = new List<Link>()
        {
            _linkBuilder.Build(Rels.Self, HttpMethods.Get, RouteNames.GetAnimal, new { Id = model.Id}),
            _linkBuilder.Build(Rels.ListAll, HttpMethods.Get, RouteNames.GetAnimals),
        };

        AddChildLink(model, links);

        return new AnimalView
        {
            Classification = model.Classification,
            Species = model.Species,
            Sound = model.Sound,
            Links = links
        };
    }

    private void AddChildLink(AnimalModel model, List<Link> links)
    {
        switch (model.Species)
        {
            case "Pigeon":
                links.Add(_linkBuilder.Build(Rels.Child, HttpMethods.Get, RouteNames.GetPigeon, new { Id = model.Id}));
                break;
            case "Dog":
                links.Add(_linkBuilder.Build(Rels.Child, HttpMethods.Get, RouteNames.GetDog, new { Id = model.Id}));
                break;
            case "Cat":
                links.Add(_linkBuilder.Build(Rels.Child, HttpMethods.Get, RouteNames.GetCat, new { Id = model.Id}));
                break;
        }
    }
}