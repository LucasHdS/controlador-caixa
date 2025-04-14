namespace Core.Common;
public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime DataCadastro { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        DataCadastro = DateTime.UtcNow;
    }

    private readonly List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}