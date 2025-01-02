using WebApi_GenericRepository.DataAccessLayer;
using WebApi_GenericRepository.DMO;

public interface IUnitOfWork : IDisposable
{

    // unit of work katmanı, repositoryleri kapsülleyen bir katmandır!!
    // servisler repositorylere direk erişmez, repository'lere, unit of work üzerinden erişirler
    // unit of work, veri tabanı işlemlerinden sorumlu bir tasarım desenidir!!

    IGenericRepository<Yazar> Author { get; }
    IGenericRepository<Kitap> Book { get; }

    Task<int> SaveChange();
}
public class UnitOfWork : IUnitOfWork
{
    // Savachange metodu içerisinde context üzerinden savechange yapılacağı için, context nesnesini dependency injection yöntemi ile alalım!

    private BookContext _context;

    public IGenericRepository<Yazar> Author { get; }

    public IGenericRepository<Kitap> Book { get; }

    public UnitOfWork(BookContext context)
    {
        _context = context;
    }
    public async Task<int> SaveChange()
    {
        // unit of work üzerinden save change işlemi yapacağız
       return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        // bellekten düşmesi için!!
        _context.Dispose();
    }
}