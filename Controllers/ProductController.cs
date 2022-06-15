using Microsoft.AspNetCore.Mvc;
using BookStore.DAO;
using BookStore.Domain;

namespace BookStore.Controllers;

[ApiController]
[Route("/product/[controller]")]
public class ProductController : ControllerBase
{

    private readonly IProductDAO<Book> productDAO = new BookDAO();


    [HttpGet(Name = "GetProduct")]
    [Route("/home")]
    public IEnumerable<IProduct> GetProductList()
    {
        List<Book> bookList = productDAO.SelectAll(); 
        // remove range
        return bookList.ToArray();
    }
}
