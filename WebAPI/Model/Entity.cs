namespace WebAPI.Model;
public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
}