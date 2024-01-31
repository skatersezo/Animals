using Animals.Core.Adaptors.Rest;

namespace Animals.API.Builders;

public interface IViewBuilder<in TModel, out TView>
{
    TView Build(TModel model);
}