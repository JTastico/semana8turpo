using System.Linq.Expressions;
using Lab8_JamilTurpo.Data; // Asegúrate que tu DbContext esté en esta carpeta
using Lab8_JamilTurpo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8_JamilTurpo.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly LINQExampleContext _context; // O el nombre que tenga tu DbContext
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(LINQExampleContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);
}