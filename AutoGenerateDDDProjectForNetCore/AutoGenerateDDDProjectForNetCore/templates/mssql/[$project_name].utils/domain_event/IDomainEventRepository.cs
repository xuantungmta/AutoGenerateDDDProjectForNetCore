namespace [$project_name].utils.domain_event
{
    public interface IDomainEventRepository
    {
        void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;

        IEnumerable<DomainEventRecord> FindAll();
    }
}