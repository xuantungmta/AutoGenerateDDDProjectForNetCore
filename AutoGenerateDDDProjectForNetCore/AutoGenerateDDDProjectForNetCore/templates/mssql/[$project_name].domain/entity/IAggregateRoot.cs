namespace blog.domain.entity
{
    public interface IAggregateRoot
    {
        long ID { get; }
		long? CreatedBy { get; }
        DateTime? CreatedDate { get; }
        DateTime? ModifiedDate { get; }
        short Status { get; }
    }
}