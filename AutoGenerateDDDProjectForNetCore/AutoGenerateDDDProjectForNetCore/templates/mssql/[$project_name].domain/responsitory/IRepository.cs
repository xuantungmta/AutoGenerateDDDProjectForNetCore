using [$project_name].domain.entity;
using System.Linq.Expressions;

namespace [$project_name].domain.responsitory
{
    public interface IRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        long Add(TEntity entity);
        long Update(TEntity entity);

        Task<long> AddAsync(TEntity entity);
        Task<long> UpdateAsync(TEntity entity);

        void Delete(long id);

        Task DeleteAsync(long id);

        void DeleteMulti(Expression<Func<TEntity, bool>> where);

        Task DeleteMultiAsync(Expression<Func<TEntity, bool>> where);

        void DeleteByStatus(long id);

        Task DeleteByStatusAsync(long id);

        /// <summary>
        /// for table with key have multi fields
        /// </summary>
        /// <param name="id"></param>
        /// <param name="otherId"></param>
        /// <returns></returns>
        Task DeleteAsync(long id, long otherId);

        TEntity GetSingleById(object id);

        Task<TEntity> GetSingleByIdAsync(object id);

        TEntity GetSingleByCondition(Expression<Func<TEntity, bool>> where);

        Task<TEntity> GetSingleByConditionAsync(Expression<Func<TEntity, bool>> where);

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetMulti(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> GetMultiAsync(Expression<Func<TEntity, bool>> where);

        IEnumerable<TEntity> GetMultiPaging(string search = "", int page = 0, int size = 50, string order = "DESC");

        Task<IEnumerable<TEntity>> GetMultiPagingAsync(string search = "", int page = 0, int size = 50, string order = "DESC");

        bool CheckContains(string query, TEntity param);

        long Count(Expression<Func<TEntity, bool>>? where);

        Task<long> CountAsync(Expression<Func<TEntity, bool>>? where);

        long CountContain(Expression<Func<TEntity, bool>>? where);

        Task<long> CountContainAsync(Expression<Func<TEntity, bool>>? where);
    }
}