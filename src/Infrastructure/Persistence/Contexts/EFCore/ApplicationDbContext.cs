using FluentPOS.Application.Abstractions.DbContexts;
using FluentPOS.Domain.Entities;
using FluentPOS.Infrastructure.Extensions;
using FluentPOS.Infrastructure.Identity;
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
        public DbSet<Product> Products { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.ApplyIdentityConfiguration();
        }
    }
}