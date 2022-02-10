using [$project_name].utils.domain_event;

namespace [$project_name].domain.responsitory
{
    public interface IDomainEventRepository
    {
        void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;

        IEnumerable<DomainEventRecord> FindAll();
    }
}