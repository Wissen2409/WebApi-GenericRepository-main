using System.Linq.Expressions;
using WebApi_GenericRepository.DMO;

public class BookService : IBookService
{
    // ben kitap için ayrı bir repository yazmadım, bunun yerine generic repository yazdım!!

    // generic repository'i hangi tip için kullanmak istediğinizi siz seçersiniz!!
    // Ben Kitap için seçeceğim!!

    // Repository'e, unit of work üzerinden erişeceğiz!!

    private IUnitOfWork _unitOfWork;
    public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<Kitap> Get()
    {
        return _unitOfWork.Book.GetAll().Result.ToList();
    }
    public async Task<int> AddBook(Kitap kitap)
    {
        _unitOfWork.Book.Add(kitap);
        return await _unitOfWork.SaveChange();
    }
    public async Task<IEnumerable<Kitap>> Find(Kitap kitap)
    {

        // Generic repository find adımında benden expressin istiyor
        // parametre olarak gelen kitap adı değerini expression haline getirelim!!

        Expression<Func<Kitap, bool>> filter = s => s.Ad == kitap.Ad;
        return await _unitOfWork.Book.Find(filter);
    }

}
public interface IBookService
{

    public List<Kitap> Get();

    public Task<int> AddBook(Kitap kitap);

    public Task<IEnumerable<Kitap>> Find(Kitap kitap);
}