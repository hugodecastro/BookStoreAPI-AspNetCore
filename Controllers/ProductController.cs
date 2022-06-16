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
        public IEnumerable<Product> GetProductList()
        {
            List<Book> bookList = productDAO.SelectAllProductsInfo();
            return bookList.ToArray();
        }
        [Route("/products/count")]
        public IActionResult GetProductCount()
        {
            int productsCount = productDAO.Count();
            return Ok(productsCount);
        }
}
