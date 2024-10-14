using IOBBank.Domain.Entidades.Base;
using IOBBank.Domain.Interfaces;
using IOBBank.Domain.Interfaces.Repositories.Base;
using IOBBank.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IOBBank.Infra.Data.Repositories.Base;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntidade
{
    private readonly IOBBankContext _context;

    protected GenericRepository(IOBBankContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public IQueryable<TEntity> Get() => _context.Set<TEntity>().AsTracking();

    public IQueryable<TEntity> GetAsNoTracking() => Get().AsNoTracking();
    public async Task<TEntity> GetByIdAsync(Guid id) => await Get().AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);

    public void Add(TEntity entity) => _context.Add(entity);

    public void AddRange(List<TEntity> entity) => _context.AddRange(entity);

    public void Update(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;

    public void Remove(TEntity entity) => _context.Remove(entity);

    public void UpdateChange(List<TEntity> entity) => _context.UpdateRange(entity); 

    public void RemoveRange(List<TEntity> entity) => _context.RemoveRange(entity);
   
}