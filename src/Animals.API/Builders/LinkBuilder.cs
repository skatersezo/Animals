using Animals.Core.Adaptors.Rest;

namespace Animals.API.Builders;

public class LinkBuilder : IAmALinkBuilder
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LinkBuilder(
        LinkGenerator linkGenerator, 
        IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
    }

    public Link Build(string rel, string method, string endpointName, object? routeValues)
    {
        return new Link(
            _linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, endpointName, routeValues),
            rel,
            method);
    }
}