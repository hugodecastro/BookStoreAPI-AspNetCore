using BookStore.DAO;

namespace BookStore.Service;

public class ReservationService
{
    ReservationDAO reservationDAO = new ReservationDAO();
    public bool CheckProductAvailability(int ProductId)
    {  
        /// <summary>
        /// Checks if product is available or not
        /// </summary>
        /// <param name="ProductId">Id of the product to be checked.</param>
        /// <returns>This method returns reservation status.</returns>

        var reservationDic = reservationDAO.SelectByProductId(ProductId);
        bool availability = false;

        return bool.TryParse(reservationDic["status"], out availability);
    }
}