using [$project_name].domain.responsitory;
using System.Data;

namespace [$project_name].infrastructure.persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
            this._id = Guid.NewGuid();
        }

        #region IUnitOfWork Members

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private Guid _id = Guid.Empty;

        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection ?? (_connection = _dbFactory.Init()); }
        }

        IDbTransaction IUnitOfWork.Transaction
        {
            get { return _transaction; }
        }

        Guid IUnitOfWork.Id
        {
            get { return _id; }
        }

        #endregion IUnitOfWork Members

        public void Begin()
        {
            if (_connection == null) _connection = _dbFactory.Init();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }
    }
}