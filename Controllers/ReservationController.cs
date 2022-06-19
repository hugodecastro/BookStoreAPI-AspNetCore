using Microsoft.AspNetCore.Mvc;
using BookStore.DAO;
using BookStore.Domain;
using BookStore.Service;
using Newtonsoft.Json;


namespace BookStore.Controllers;

[ApiController]
[Route("/reservation/[controller]")]
public class ReservationController : ControllerBase
{

    private readonly ReservationDAO reservationDAO = new ReservationDAO();


    [HttpGet(Name = "GetReservation")]
        [Route("/reservation")]
        public IEnumerable<Reservation> GetReservationList()
        {
            List<Reservation> reservationList = reservationDAO.SelectAll();
            return reservationList.ToArray();
        }
        [Route("/reservation/{ProductName:alpha}")]
        public IActionResult GetReservation(string ProductName)
        {
            Dictionary<string, string> ProductReservation = reservationDAO.SelectProductReservationByName(ProductName);
            return Ok(ProductReservation);
        }
        [Route("/reservation/count")]
        public IActionResult GetProductCount()
        {
            int productsCount = reservationDAO.ReservationsCount();
            return Ok(productsCount);
        }

    [HttpPost(Name = "PostReservation")]
    [Route("/reservation/post/{ProductName:alpha}")]
    public IActionResult PostReservation(string ProductName)
    {
        String responseMsg = "";
        IProductDAO<Product> productDAO = Product.getProductDAOFactory("Book") 
                                            ?? throw new NullReferenceException("Inform a valid product category!");
        ReservationService reservationService = new ReservationService();

        if(!reservationService.CheckProductAvailability(ProductName))
        {
            Book book = (Book) productDAO.SelectProductInfoByName(ProductName);
            responseMsg = $"Product {book.Name} reserved successfully!";
            if(reservationDAO.Insert(book.ProductId))
                return Content(JsonConvert.SerializeObject(responseMsg), "text/plain");
        }
        responseMsg = "Product unavailable!";
        return Content(JsonConvert.SerializeObject(responseMsg), "text/plain");
    }
}
