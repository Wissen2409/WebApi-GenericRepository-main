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

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);

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
    public void Add(T entity)
    {
        _dbSet.AddAsync(entity);
        
    }
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);

        // save change kısımlarını unit work içerisinde yaparız!
     
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
    public void Update(T entity)
    {
        _dbSet.Update(entity);
     
    }

  
}