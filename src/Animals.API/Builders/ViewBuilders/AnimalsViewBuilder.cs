using Animals.API.Views;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Domain.Models;

namespace Animals.API.Builders.ViewBuilders;

public class AnimalsViewBuilder : AbstractViewBuilder<List<AnimalModel>, AnimalsView>
{
    private readonly IViewBuilder<AnimalModel, AnimalView> _animalViewBuilder;
    
    public AnimalsViewBuilder(
        IAmALinkBuilder linkBuilder, 
        IViewBuilder<AnimalModel, AnimalView> animalViewBuilder) 
        : base(linkBuilder)
    {
        _animalViewBuilder = animalViewBuilder;
    }

    public override AnimalsView Build(List<AnimalModel> models)
    {
        List<AnimalView> animalViews = new();

        foreach (var animalModel in models)
        {
            animalViews.Add(_animalViewBuilder.Build(animalModel));
        }

        return new AnimalsView
        {
            AnimalViews = animalViews,
            Links = new List<Link>
            {
                _linkBuilder.Build(Rels.Self, HttpMethods.Get, RouteNames.GetAnimals)
            }
        };
    }
}