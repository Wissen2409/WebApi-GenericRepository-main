using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApi_GenericRepository.DataAccessLayer;

public interface IGenericRepository<T>where T:class
{
    // id değerine göre sonuç getirme
    Task<T> GetById(int id);

    // tüm kayıtları getirir!!
    Task<IEnumerable<T>> GetAll();

    // gönderilen expression'a göre istenilen veriler getirilir!!
    Task<IEnumerable<T>> Find(Expression<Func<T,bool>> predicate);

    Task Add(T entity);

    bool Update(T entity);

    bool Delete(T entity);

}

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BookContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(BookContext context)
    {
        _context=context;
        _dbSet=_context.Set<T>();
    }
    public async Task Add(T entity)
    {
        _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
     
    }
    public bool Delete(T entity)
    {
        _dbSet.Remove(entity);
       int returnValue =  _context.SaveChanges();
       return returnValue>=1;
    }
    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task<T> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    public bool Update(T entity)
    {
        _dbSet.Update(entity);
       int result =  _context.SaveChanges();
       return result>=1;
    }

  
}