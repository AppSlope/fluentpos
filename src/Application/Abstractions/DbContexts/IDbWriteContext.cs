using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Application.Abstractions.DbContexts
{
    //This context will use an instance of ApplicationDbContext (EfCore) so that it can execute Dapper commands within the same SQL connection as EFCore.
    //This greatly helps in using both EFCore and Dapper in the same SQL transaction, thus helps in Rollbacks and stuff.
    //Note that we are also inheriting the read methods from IDapperDbReadContext
    //This gives us the flexibility to use the query operations using the EfCore context instance too.
    //It will help when you start switching EFCore DB Provider from the default SQL to MYSQL or so on.
    public interface IDbWriteContext : IDbReadContext
    {
#nullable enable

        Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

#nullable disable
    }
}