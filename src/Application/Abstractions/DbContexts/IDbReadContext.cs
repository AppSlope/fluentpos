using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Application.Abstractions.DbContexts
{
    //This can be used to execute Dapper Queries (queries only!).
    //The issue with adding Dapper Command Methods here is that, it will always use a SQL connection instance that is different from the EF Core instance.
    //Due to this, there can be data inconsistencies and you will not be able to rollback transactions.
    //Thus, don't add any Dapper Commands here, Just use this context only to query data from the database.
    //We will have a seperate Dapper Context that deals only with writing to the database against the same sql connection instance that is being used by EFCore's ApplicationDbContext.
    public interface IDbReadContext
    {
#nullable enable

        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

#nullable disable
    }
}