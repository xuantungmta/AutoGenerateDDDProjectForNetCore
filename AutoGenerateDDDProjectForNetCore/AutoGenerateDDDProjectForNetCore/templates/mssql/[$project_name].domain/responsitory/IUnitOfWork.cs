using System.Data;

namespace [$project_name].domain.responsitory
{
    public interface IUnitOfWork : IDisposable
    {
        Guid Id { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        void Begin();

        void Commit();

        void Rollback();
    }
}