using Animals.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Animals.Core.Adaptors.Db;

public class AnimalContext : DbContext
{
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Pigeon> Pigeons { get; set; }
    
    
    public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseInMemoryDatabase("AnimalsDB");
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(e =>
        {
            e.Property(p => p.Id).ValueGeneratedOnAdd();
            e.HasKey(p => p.Id);
        });
        
        modelBuilder.Entity<Dog>(e =>
        {
            e.Property(d => d.Name).IsRequired(false);
        });

        modelBuilder.Entity<Cat>(e =>
        {
            e.Property(c => c.FavouriteToy).IsRequired(false);
        });

        modelBuilder.Entity<Pigeon>(e =>
        {
            e.Property(p => p.Colour).IsRequired(false);
        });
    }

    public void SeedData()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "animalData.json");
        var jsonData = File.ReadAllText(filePath);
        var animalsData = JsonConvert.DeserializeObject<List<AnimalData>>(jsonData);
        
        foreach (var animalData in animalsData)
        {
            switch (animalData.Type)
            {
                case "Dog":
                    var dog = new Dog(animalData.Name);
                    Dogs.Add(dog);
                    break;

                case "Pigeon":
                    var pigeon = new Pigeon(animalData.Colour);
                    Pigeons.Add(pigeon);
                    break;

                case "Cat":
                    var cat = new Cat(animalData.FavouriteToy);
                    Cats.Add(cat);
                    break;
            }
        }

        SaveChanges();
    }
}