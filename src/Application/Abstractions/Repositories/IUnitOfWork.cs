using FluentPOS.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> CommitAsync(CancellationToken cancellationToken);

        Task Rollback();
    }
}