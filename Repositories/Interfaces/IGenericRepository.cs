// Archivo: Repositories/Interfaces/IGenericRepository.cs
using System.Linq.Expressions;

namespace Lab8_JamilTurpo.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    // Este método es clave para usar LINQ de forma flexible
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}