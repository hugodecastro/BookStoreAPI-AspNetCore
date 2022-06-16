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
        public IEnumerable<Product> GetProductsList()
        {
            List<Book> bookList = productDAO.SelectAllProductsInfo();
            return bookList.ToArray();
        }
        [Route("/product/{ProductName:alpha}")]
        public IActionResult GetProduct(string ProductName)
        {
            Book book = productDAO.SelectProductInfoByName(ProductName);
            return Ok(book);
        }
        [Route("/products/count")]
        public IActionResult GetProductCount()
        {
            int productsCount = productDAO.ProductCount();
            return Ok(productsCount);
        }
}
