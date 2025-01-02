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

}
public interface IBookService
{

    public List<Kitap> Get();
}