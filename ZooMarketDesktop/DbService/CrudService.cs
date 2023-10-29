using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZooMarketDesktop.DbService.Exception;

namespace ZooMarketDesktop.DbService;

internal class CrudService<TId, TEntity> : IDisposable where TEntity : class
{
    private readonly DbContext _context;

    public CrudService(DbContext context) => _context = context;

    public async Task<List<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

    public async Task<TEntity> GetAsync(TId id) => await FindByIdAsync(id);

    public async Task CreateAsync(TEntity entity)
    {
        var result = _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TId id)
    {
        _context.Set<TEntity>().Remove(await FindByIdAsync(id));
        await _context.SaveChangesAsync();
    }

    private async Task<TEntity> FindByIdAsync(TId id) =>
        await _context.FindAsync<TEntity>(id) ?? throw new EntityNotFound();

    public void Dispose()
    {
        _context.Dispose();
    }
}
