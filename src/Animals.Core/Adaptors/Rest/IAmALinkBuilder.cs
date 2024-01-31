namespace Animals.Core.Adaptors.Rest;

public interface IAmALinkBuilder
{
    Link Build(string rel, string method, string endpointName, object? routeValues = null);
}