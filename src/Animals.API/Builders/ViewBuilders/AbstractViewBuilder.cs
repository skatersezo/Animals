using Animals.Core.Adaptors.Rest;

namespace Animals.API.Builders.ViewBuilders;

public abstract class AbstractViewBuilder<TModel, TView> : IViewBuilder<TModel, TView>
{
    protected readonly IAmALinkBuilder _linkBuilder;

    protected AbstractViewBuilder(IAmALinkBuilder linkBuilder)
    {
        _linkBuilder = linkBuilder;
    }

    public abstract TView Build(TModel model);
}