
using WebApi_GenericRepository.DMO;

public interface IAuthorService{

    public List<Yazar> Get();

}
public class AuthorService : IAuthorService
{
    private IUnitOfWork _unitOfWork;
    public AuthorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<Yazar> Get()
    {
        return _unitOfWork.Author.GetAll().Result.ToList();
        _unitOfWork.SaveChange();
    }
}