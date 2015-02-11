#region Using directives

using System.Threading;
using System.Threading.Tasks;
using Skarpline.PersistenceLayer.Repository.Repositories;

#endregion

namespace Skarpline.PersistenceLayer.Repository.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IGenericRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
    }
}