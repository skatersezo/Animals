using Animals.API.Builders;
using Animals.API.Builders.ViewBuilders;
using Animals.API.Views;
using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Domain.Models;
using Animals.Core.Ports.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Darker.AspNetCore;
using Paramore.Darker.Policies;
using Paramore.Darker.QueryLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnimalsAPI", Version = "V1" });
});

builder.Services.AddDbContext<AnimalContext>(options => options.UseInMemoryDatabase("AnimalsDB"));
builder.Services.AddScoped<IAmALinkBuilder, LinkBuilder>();
builder.Services.AddHttpContextAccessor();

// Register all implementations of IViewBuilder
var viewBuilderInterfaceType = typeof(IViewBuilder<,>);
var viewBuilderTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => p.IsClass && !p.IsAbstract && p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == viewBuilderInterfaceType));

foreach (var viewBuilderType in viewBuilderTypes)
{
    foreach (var interfaceType in viewBuilderType.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == viewBuilderInterfaceType))
    {
        builder.Services.AddScoped(interfaceType, viewBuilderType);
    }
}

builder.Services.AddBrighter()
    .AutoFromAssemblies();

builder.Services.AddDarker()
    .AddHandlersFromAssemblies(typeof(AnimalByIdQuery).Assembly)
    .AddJsonQueryLogging()
    .AddDefaultPolicies();

builder.Services.AddCors(opt =>
    opt.AddPolicy("AllowAll", policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
    
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AnimalContext>();
    context.Database.EnsureCreated();
    context.SeedData();
}

app.Run();