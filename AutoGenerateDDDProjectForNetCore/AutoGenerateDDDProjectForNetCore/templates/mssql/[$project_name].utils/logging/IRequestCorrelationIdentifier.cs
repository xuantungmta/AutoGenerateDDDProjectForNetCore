namespace [$project_name].utils.logging
{
    public interface IRequestCorrelationIdentifier
    {
        string CorrelationID { get; }
    }
}