using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using IOBBank.Domain.Entidades.Base;
using IOBBank.Domain.Interfaces;
using IOBBank.Domain.Entidades;

namespace IOBBank.Infra.Data.Context
{
    public class IOBBankContext : DbContext, IUnitOfWork
    {
        public IOBBankContext(DbContextOptions<IOBBankContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            PreenchePropriedadesAuditaveis();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void PreenchePropriedadesAuditaveis()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        PreencheDataCriacao(entry);
                        break;
                    case EntityState.Modified:
                        PreencheDataAtualizacao(entry);
                        break;
                }
            }
        }

        private static void PreencheDataCriacao(EntityEntry entry)
        {
            if (entry.Entity.GetType().GetProperty(nameof(Entidade.DataCriacao)) != null)
                entry.Property(nameof(Entidade.DataCriacao)).CurrentValue = DateTime.UtcNow;
        }

        private static void PreencheDataAtualizacao(EntityEntry entry)
        {
            if (entry.Entity.GetType().GetProperty(nameof(Entidade.DataAlteracao)) != null)
                entry.Property(nameof(Entidade.DataAlteracao)).CurrentValue = DateTime.UtcNow;
        }

        public void Dispose()
        {
            // Implemente a lógica de liberação de recursos, se necessário.
            base.Dispose();
        }
    }
}
