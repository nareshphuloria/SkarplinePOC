#region Using directives

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Skarpline.PersistenceLayer.Repository.Repositories
{
    public interface IGenericRepositoryAsync<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> whereCondition);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> lambdaExpression, Expression<Func<TEntity, int>> orderByexpr, bool orderByDesc = false);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> lambdaExpression);
        Task<List<TEntity>> GetListAsync(string includeTableName, Expression<Func<TEntity, bool>> lambdaExpression);
    }
}