using Animals.Core.Ports.Repositories;

namespace Animals.Core.Domain;

public abstract class Animal : IEntity
{
    public int Id { get; set; }
    public virtual string Classification => "Unknown";
    public virtual string Species => GetType().Name;
    public abstract string Sound { get; }
}