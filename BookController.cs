using Microsoft.AspNetCore.Mvc;
using WebApi_GenericRepository.DMO;


[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{

    private IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _bookService.Get();

        if (result.Count != 0)
        {
            return Ok(result);
        }
        else
        {
            return NoContent();
        }
    }
    [HttpPost]
    public IActionResult Post(KitapViewModel model)
    {

        Kitap k = new Kitap
        {
            Ad = model.Ad,
            //Kitapno = model.Kitapno,
            Puan = model.Puan,
            Turno = model.Turno,
            Yazarno = model.YazarNo,
            Sayfasayisi = model.SayfaSayisi,
            

        };
        var value = _bookService.AddBook(k).Result;
        return Ok(value);
    }
    [Route("find")]
    [HttpPost]
    public IActionResult Find(KitapViewModel model){

       var returnValue = _bookService.Find(new Kitap(){ Ad=model.Ad}).Result.ToList();
       return Ok(returnValue);
    }
}