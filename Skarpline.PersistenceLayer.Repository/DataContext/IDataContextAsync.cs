#region Using directives

using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Skarpline.PersistenceLayer.Repository.DataContext
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}