using Animals.API.Builders;
using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Ports.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Darker.AspNetCore;
using Paramore.Darker.Policies;
using Paramore.Darker.QueryLogging;

namespace Animals.API;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnimalsAPI", Version = "V1" });
        });

        services.AddDbContext<AnimalContext>(options => options.UseInMemoryDatabase("AnimalsDB"));
        services.AddScoped<IAmALinkBuilder, LinkBuilder>();
        services.AddHttpContextAccessor();

        var viewBuilderInterfaceType = typeof(IViewBuilder<,>);
        var viewBuilderTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p.IsClass && !p.IsAbstract && p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == viewBuilderInterfaceType));

        foreach (var viewBuilderType in viewBuilderTypes)
        {
            foreach (var interfaceType in viewBuilderType.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == viewBuilderInterfaceType))
            {
                services.AddScoped(interfaceType, viewBuilderType);
            }
        }

        services.AddBrighter()
            .AutoFromAssemblies();

        services.AddDarker()
            .AddHandlersFromAssemblies(typeof(AnimalByIdQuery).Assembly)
            .AddJsonQueryLogging()
            .AddDefaultPolicies();

        services.AddCors(opt =>
            opt.AddPolicy("AllowAll", policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseCors("AllowAll");

        using (var scope = app.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AnimalContext>();
            context.Database.EnsureCreated();
            context.SeedData();
        }
    }
}