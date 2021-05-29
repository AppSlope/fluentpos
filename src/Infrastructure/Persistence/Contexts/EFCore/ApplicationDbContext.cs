using FluentPOS.Application.Abstractions.EFContexts;
using FluentPOS.Infrastructure.Identity;
using FluentPOS.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Infrastructure.Persistence.Contexts.EFCore
{
    //Read details about this implmentation in the interface cs file.
    public class ApplicationDbContext : IdentityDbContext<ExtendedIdentityUser, ExtendedIdentityRole, int>, IApplicationDbContext
    {
        public IDbConnection Connection => Database.GetDbConnection();

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}