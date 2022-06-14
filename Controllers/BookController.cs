using Microsoft.AspNetCore.Mvc;
using BookStore.DAO;
using BookStore.Domain;

namespace BookStore.Controllers;

[ApiController]
[Route("/book/[controller]")]
public class BookController : ControllerBase
{

    private readonly BookDAO bookDAO = new BookDAO();


    [HttpGet(Name = "GetBook")]
        [Route("/home")]
        public IEnumerable<Book> GetBookList()
        {
            var book = bookDAO.SelectAll();
            // remove range
            return Enumerable.Range(1, 5).Select(index => new Book
            {
                id = Int32.Parse(book["id"]),
                name = book["name"],
                year = book["year"],
                author = book["author"]
            })
            .ToArray();
        }
        [Route("/book/info")]
        public IActionResult Get()
        {   
            var book = bookDAO.SelectByName("Sidarta");
            return Ok(book.ToString());
        }
}
