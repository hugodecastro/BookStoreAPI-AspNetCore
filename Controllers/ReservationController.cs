using Microsoft.AspNetCore.Mvc;
using BookStore.DAO;
using BookStore.Domain;
using BookStore.Service;

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
        [Route("/reservation/{ProductId:int}")]
        public IActionResult GetReservation(int ProductId)
        {
            Dictionary<string, string> reservation = reservationDAO.SelectByProductId(ProductId);
            return Ok(reservation);
        }
        [Route("/reservation/count")]
        public IActionResult GetProductCount()
        {
            int productsCount = reservationDAO.ReservationsCount();
            return Ok(productsCount);
        }

    [HttpPost(Name = "PostReservation")]
    [Route("/reservation/post/{ProductId:int}")]
    public IActionResult PostReservation(int ProductId)
    {
        ReservationService reservationService = new ReservationService();
        if(!reservationService.CheckProductAvailability(ProductId))
        {
            if(reservationDAO.Insert(ProductId))
                return Ok(reservationDAO.SelectByProductId(ProductId));
        }
        return Ok("Reservation not possible: Product already reserved!");
    }
}
