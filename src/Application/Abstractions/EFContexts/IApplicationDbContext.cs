using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Application.Abstractions.EFContexts
{
    //This will be the primary DB Access Context.
    public interface IApplicationDbContext
    {
        IDbConnection Connection { get; }
        DatabaseFacade Database { get; }

        EntityEntry Entry(object entity);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}