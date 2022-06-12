using Microsoft.AspNetCore.Mvc;
using BookStore.DAO;
namespace BookStore.Controllers;

[ApiController]
[Route("/book/[controller]")]
public class BookController : ControllerBase
{

    private readonly BookDAO bookDAO = new BookDAO();


    [HttpGet(Name = "GetBook")]
        // [Route("/book/obj")]
        // public Book GetObj()
        // {
        //     // return 
        //     // return book;
        // }
        [Route("/book/info")]
        public IActionResult Get()
        {   
            var book = bookDAO.SelectByName("Lobo da Estepe");
            return Ok(book.ToString());
        }
}
