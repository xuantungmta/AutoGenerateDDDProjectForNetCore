using [$project_name].domain.entity;
using [$project_name].domain.responsitory;
using [$project_name].utils.helpers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace [$project_name].infrastructure.persistence
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        protected IUnitOfWork unitOfWork;

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public long Add(TEntity entity)
        {
            string query = @"EXEC dbo." + typeof(TEntity).Name + "_Insert ";
            PropertyInfo[] props = typeof(TEntity).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                query += $"@{props[i].Name} = @{props[i].Name}";
                if (i != props.Length - 1) query += ", ";
            }

            return unitOfWork.Connection.QueryFirst<long>(query, entity, unitOfWork.Transaction);
        }

        public async Task<long> AddAsync(TEntity entity)
        {
            string query = @"EXEC dbo." + typeof(TEntity).Name + "_Insert ";
            PropertyInfo[] props = typeof(TEntity).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                query += $"@{props[i].Name} = @{props[i].Name}";
                if (i != props.Length - 1) query += ", ";
            }

            return await unitOfWork.Connection.QueryFirstAsync<int>(query, entity, unitOfWork.Transaction);
        }

        public long Update(TEntity entity)
        {
            string query = @"EXEC dbo." + typeof(TEntity).Name + "_Update ";
            PropertyInfo[] props = typeof(TEntity).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                query += $"@{props[i].Name} = @{props[i].Name}";
                if (i != props.Length - 1) query += ", ";
            }

            return unitOfWork.Connection.QueryFirst<long>(query, entity, unitOfWork.Transaction);
        }

        public async Task<long> UpdateAsync(TEntity entity)
        {
            string query = @"EXEC dbo." + typeof(TEntity).Name + "_Update ";
            PropertyInfo[] props = typeof(TEntity).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                query += $"@{props[i].Name} = @{props[i].Name}";
                if (i != props.Length - 1) query += ", ";
            }

            return await unitOfWork.Connection.QueryFirstAsync<int>(query, entity, unitOfWork.Transaction);
        }

        public bool CheckContains(string query, TEntity param)
        {
            return unitOfWork.Connection.QueryFirst<bool>(query, param, unitOfWork.Transaction);
        }

        public long Count(Expression<Func<TEntity, bool>>? where)
        {
            long count = -1;
            if (where != null)
            {
                ExpressionObjectValue memberExtractor = ObjectValue.GetMemberName(where);
                string query = @"SELECT COUNT(*) FROM " + typeof(TEntity).Name + " WHERE " + memberExtractor.MemberName + memberExtractor.Operator + "=@Value";
                count = unitOfWork.Connection.QueryFirst<long>(query, new
                {
                    Value = memberExtractor.Value
                }, unitOfWork.Transaction);
            }
            else
            {
                string query = @"SELECT COUNT(*) FROM " + typeof(TEntity).Name;
                count = unitOfWork.Connection.QueryFirst<long>(query, null, unitOfWork.Transaction);
            }
            return count;
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? where)
        {
            long count = -1;
            if (where != null)
            {
                ExpressionObjectValue memberExtractor = ObjectValue.GetMemberName(where);
                string query = @"SELECT COUNT(*) FROM " + typeof(TEntity).Name + " WHERE " + memberExtractor.MemberName + memberExtractor.Operator + "=@Value";
                count = await unitOfWork.Connection.QueryFirstAsync<long>(query, new
                {
                    Value = memberExtractor.Value
                }, unitOfWork.Transaction);
            }
            else
            {
                string query = @"SELECT COUNT(*) FROM " + typeof(TEntity).Name;
                count = await unitOfWork.Connection.QueryFirstAsync<long>(query, null, unitOfWork.Transaction);
            }
            return count;
        }

        public void Delete(long id)
        {
            string query = @"EXEC " + typeof(TEntity).Name + "_Delete @ID=" + id;
            unitOfWork.Connection.Execute(query, null, unitOfWork.Transaction);
        }

        public async Task DeleteAsync(long id)
        {
            string query = @"EXEC " + typeof(TEntity).Name + "_Delete @ID=" + id;
            await unitOfWork.Connection.ExecuteAsync(query, null, unitOfWork.Transaction);
        }

        public async Task DeleteAsync(long id, long otherId)
        {
            string query = @"EXEC " + typeof(TEntity).Name + "_Delete @ID=" + id + ", @ID2=" + otherId;
            await unitOfWork.Connection.ExecuteAsync(query, null, unitOfWork.Transaction);
        }

        public void DeleteByStatus(long id)
        {
            string query = @"EXEC " + typeof(TEntity).Name + "_DeleteByStatus @ID=" + id;
            unitOfWork.Connection.Execute(query, null, unitOfWork.Transaction);
        }

        public async Task DeleteByStatusAsync(long id)
        {
            string query = @"EXEC " + typeof(TEntity).Name + "_DeleteByStatus @ID=" + id;
            await unitOfWork.Connection.ExecuteAsync(query, null, unitOfWork.Transaction);
        }

        public void DeleteMulti(Expression<Func<TEntity, bool>> where)
        {
            ExpressionObjectValue memberExtractor = ObjectValue.GetMemberName(where);
            string query = @"EXEC " + typeof(TEntity).Name + "_DeleteMultiBy_" + memberExtractor.MemberName + $"@{memberExtractor.MemberName}=@Value";
            unitOfWork.Connection.Query<TEntity>(query, new
            {
                Value = memberExtractor.Value
            }, unitOfWork.Transaction);
        }

        public async Task DeleteMultiAsync(Expression<Func<TEntity, bool>> where)
        {
            ExpressionObjectValue memberExtractor = ObjectValue.GetMemberName(where);
            string query = @"EXEC " + typeof(TEntity).Name + "_DeleteMultiBy_" + memberExtractor.MemberName + $"@{memberExtractor.MemberName}=@Value";
            await unitOfWork.Connection.QueryAsync<TEntity>(query, new
            {
                Value = memberExtractor.Value
            }, unitOfWork.Transaction);
        }

        public IEnumerable<TEntity> GetAll()
        {
            string query = @"EXEC " + typeof(TEntity).Name + "_GetAll";
            return unitOfWork.Connection.Query<TEntity>(query, null, unitOfWork.Transaction);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            string query = @"EXEC " + typeof(TEntity).Name + "_GetAll";
            return await unitOfWork.Connection.QueryAsync<TEntity>(query, null, unitOfWork.Transaction);
        }

        public IEnumerable<TEntity> GetMulti(Expression<Func<TEntity, bool>> where)
        {
            ExpressionObjectValue memberExtractor = ObjectValue.GetMemberName(where);
            string query = @"EXEC " + typeof(TEntity).Name + "_GetMutilBy_" + memberExtractor.MemberName + $" @{memberExtractor.MemberName}=@Value";
            return unitOfWork.Connection.Query<TEntity>(query, new
            {
                Value = memberExtractor.Value
            }, unitOfWork.Transaction);
        }

        public async Task<IEnumerable<TEntity>> GetMultiAsync(Expression<Func<TEntity, bool>> where)
        {
            ExpressionObjectValue memberExtractor = ObjectValue.GetMemberName(where);
            string query = @"EXEC " + typeof(TEntity).Name + "_GetMutilBy_" + memberExtractor.MemberName + $" @{memberExtractor.MemberName}=@Value";
            return await unitOfWork.Connection.QueryAsync<TEntity>(query, new
            {
                Value = memberExtractor.Value
            }, unitOfWork.Transaction);
        }

        public IEnumerable<TEntity> GetMultiPaging(string search = "", int page = 0, int size = 50, string order = "DESC")
        {
            if (order != "DESC" && order != "ASC")
                throw new InvalidDataException("Order must be ASC or DESC");
            string query = $"EXEC {typeof(TEntity).Name}_GetAll_Paging @Keyword=@Keyword, @Page=@Page, @PageSize=@PageSize, @Order=@Order";
            return unitOfWork.Connection.Query<TEntity>(query,
                new
                {
                    Keyword = search,
                    Page = page,
                    PageSize = size,
                    Order = order
                }, unitOfWork.Transaction);
        }

        public async Task<IEnumerable<TEntity>> GetMultiPagingAsync(string search = "", int page = 0, int size = 50, string order = "DESC")
        {
            if (order != "DESC" && order != "ASC")
                throw new InvalidDataException("Order must be ASC or DESC");
            string query = $"EXEC {typeof(TEntity).Name}_GetAll_Paging @Keyword=@Keyword, @Page=@Page, @PageSize=@PageSize, @Order=@Order";
            return await unitOfWork.Connection.QueryAsync<TEntity>(query,
                new
                {
                    Keyword = search,
                    Page = page,
                    PageSize = size,
                    Order = order
                }, unitOfWork.Transaction);
        }

        public TEntity GetSingleByCondition(Expression<Func<TEntity, bool>> where)
        {
            ExpressionObjectValue memberExtractor = ObjectValue.GetMemberName(where);
            string query = $"SELECT * FROM {typeof(TEntity).Name} WHERE " + memberExtractor.MemberName + "=@Value";
            return unitOfWork.Connection.QueryFirstOrDefault<TEntity>(query, new
            {
                Value = memberExtractor.Value
            }, unitOfWork.Transaction);
        }

        public async Task<TEntity> GetSingleByConditionAsync(Expression<Func<TEntity, bool>> where)
        {
            ExpressionObjectValue memberExtractor = ObjectValue.GetMemberName(where);
            string query = $"SELECT * FROM {typeof(TEntity).Name} WHERE " + memberExtractor.MemberName + "=@Value";
            return await unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(query, new
            {
                Value = memberExtractor.Value
            }, unitOfWork.Transaction);
        }

        public TEntity GetSingleById(object id)
        {
            string query = @"EXEC " + typeof(TEntity).Name + "_GetByID @ID=" + id;
            return unitOfWork.Connection.QueryFirstOrDefault<TEntity>(query, null, unitOfWork.Transaction);
        }

        public async Task<TEntity> GetSingleByIdAsync(object id)
        {
            string query = @"EXEC " + typeof(TEntity).Name + "_GetByID @ID=" + id;
            return await unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(query, null, unitOfWork.Transaction);
        }

        public long CountContain(Expression<Func<TEntity, bool>>? where)
        {
            long count = -1;
            if (where != null)
            {
                ExpressionObjectValue memberExtractor = ObjectValue.GetMemberNameOfContain(where);
                string query = @"SELECT COUNT(*) FROM " + typeof(TEntity).Name + " WHERE " + memberExtractor.MemberName + memberExtractor.Operator + "@Value";
                count = unitOfWork.Connection.QueryFirst<long>(query, new
                {
                    Value = memberExtractor.Value
                }, unitOfWork.Transaction);
            }
            else
            {
                string query = @"SELECT COUNT(*) FROM " + typeof(TEntity).Name;
                count = unitOfWork.Connection.QueryFirst<long>(query, null, unitOfWork.Transaction);
            }
            return count;
        }

        public async Task<long> CountContainAsync(Expression<Func<TEntity, bool>>? where)
        {
            long count = -1;
            if (where != null)
            {
                ExpressionObjectValue memberExtractor = ObjectValue.GetMemberNameOfContain(where);
                string query = @"SELECT COUNT(*) FROM " + typeof(TEntity).Name + " WHERE " + memberExtractor.MemberName + memberExtractor.Operator + "@Value";
                count = await unitOfWork.Connection.QueryFirstAsync<long>(query, new
                {
                    Value = memberExtractor.Value
                }, unitOfWork.Transaction);
            }
            else
            {
                string query = @"SELECT COUNT(*) FROM " + typeof(TEntity).Name;
                count = await unitOfWork.Connection.QueryFirstAsync<long>(query, null, unitOfWork.Transaction);
            }
            return count;
        }
    }
}
