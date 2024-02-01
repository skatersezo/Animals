namespace Animals.Core.Adaptors.Rest;

public interface IViewBuilder<in TModel, out TView>
{
    TView Build(TModel model);
}