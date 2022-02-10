using [$project_name].utils.logging;

namespace [$project_name].utils.domain_event
{
    public class DomainEventHandle<TDomainEvent> : Handles<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        private IDomainEventRepository domainEventRepository;
        private IRequestCorrelationIdentifier requestCorrelationIdentifier;

        public DomainEventHandle(IDomainEventRepository domainEventRepository,
            IRequestCorrelationIdentifier requestCorrelationIdentifier)
        {
            this.domainEventRepository = domainEventRepository;
            this.requestCorrelationIdentifier = requestCorrelationIdentifier;
        }

        public void Handle(TDomainEvent @event)
        {
            @event.Flatten();
            @event.CorrelationID = this.requestCorrelationIdentifier.CorrelationID;
            this.domainEventRepository.Add(@event);
        }
    }
}