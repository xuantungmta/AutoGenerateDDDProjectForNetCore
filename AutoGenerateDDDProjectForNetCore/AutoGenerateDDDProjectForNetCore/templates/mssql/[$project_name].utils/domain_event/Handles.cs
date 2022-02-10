namespace [$project_name].utils.domain_event
{
    public interface Handles<T>
        where T : DomainEvent
    {
        void Handle(T args);
    }
}