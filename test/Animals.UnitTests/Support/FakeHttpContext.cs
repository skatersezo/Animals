using Microsoft.AspNetCore.Http;

namespace Animals.UnitTests.Support;

public class FakeHttpContext : IHttpContextAccessor
{
    public HttpContext? HttpContext { get; set; } = new DefaultHttpContext();
}