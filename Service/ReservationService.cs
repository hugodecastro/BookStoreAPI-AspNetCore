using BookStore.DAO;

namespace BookStore.Service;

public class ReservationService
{
    ReservationDAO reservationDAO = new ReservationDAO();
    public bool CheckProductAvailability(string ProductName)
    {  
        /// <summary>
        /// Checks if product is available or not
        /// </summary>
        /// <param name="ProductName">Name of the product to be checked.</param>
        /// <returns>This method returns reservation status.</returns>

        var reservationDic = reservationDAO.SelectProductByName(ProductName);
        bool availability = false;

        return bool.TryParse(reservationDic["status"], out availability);
    }
}