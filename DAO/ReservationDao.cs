using MySql.Data.MySqlClient;
using BookStore.Util;
using BookStore.Domain;
using Serilog;

namespace BookStore.DAO;

public class ReservationDAO
{
    private List<Reservation> reservationsList = new List<Reservation>();
    DbConnection? reservationDAOConn;

    public bool Insert(int productId)
    {
        /// <summary>
        /// Insert new reservation.
        /// </summary>
        /// <param name="productId">Id of the product being reserved.</param>
        /// <returns>This method returns TRUE if insert succeeded else FALSE.</returns>

        reservationDAOConn = new DbConnection();
        string query = $"INSERT INTO {Constants.ReservationsTableName} " + 
                        "(ProductId, Date, Status) " +
                        $"VALUES({productId}, {DateTime.Now.ToString(@"MM\/dd\/yyyy")}, TRUE)";
        
        if(reservationDAOConn.OpenConnection())
        {
            reservationDAOConn.ExecuteCommand(query);
            Log.Information("New reservation inserted successfully");
            return true;
        }
        return false;
    }

    public bool Update(int productId, string status)
    {
        /// <summary>
        /// update reservation.
        /// </summary>
        /// <param name="productId">Id of the product reserved.</param>
        /// <param name="status">New status of the reservation.</param>
        /// <returns>This method returns TRUE if update succeeded else FALSE.</returns>

        reservationDAOConn = new DbConnection();
        string query = $"UPDATE {Constants.ReservationsTableName} " + 
                       $"SET Status={status} " +
                       $"WHERE p.ProductId={productId}";
        
        if(reservationDAOConn.OpenConnection())
        {
            reservationDAOConn.ExecuteCommand(query);
            Log.Information("New reservation inserted successfully");
            return true;
        }
        return false;
    }

    public List<Reservation> SelectAll()
    {
        /// <summary>
        /// Select all reservations.
        /// </summary>
        /// <returns>List of reservations.</returns>

        reservationDAOConn = new DbConnection();
        string query = $"SELECT * FROM {Constants.ReservationsTableName};";

        if (reservationDAOConn.OpenConnection())
        {
            //Create Command
            MySqlCommand cmd = reservationDAOConn.GetCommand(query);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                Reservation reservation = new Reservation();
                reservation.ReservationId = (int) dataReader["ReservationId"];
                reservation.ProductId = (int) dataReader["ProductId"];
                reservation.Date = dataReader["Date"].ToString() + "";
                reservation.Status = (bool) dataReader["Status"];
                reservationsList.Add(reservation);
            }

            //close Data Reader
            dataReader.Close();

            reservationDAOConn.CloseConnection();
        }

        return reservationsList;
    } 

    public Dictionary<string, string> SelectByProductName(string productName)
    {
        /// <summary>
        /// Select reservation by product name.
        /// </summary>
        /// <param name="productName">Name of the product reserved.</param>
        /// <returns>Dictionary of the composed reservation and product name.</returns>

        Dictionary<string, string> reservation  = new Dictionary<string, string>()
        {
            {"id", ""},
            {"productName", ""},
            {"date", ""},
            {"status", ""}
        };
        reservationDAOConn = new DbConnection();
        string query = "SELECT r.ReservationId, p.ProductName, r.ReservationDate, r.ReservationStatus" +
                       $"FROM {Constants.ReservationsTableName} r" +
                       $"LEFT JOIN {Constants.ProductsTableName} p" +
                       "ON r.ProductId = p.ProductId" +
                       $"WHERE p.ProductName={productName}";

        if (reservationDAOConn.OpenConnection())
        {
            //Create Command
            MySqlCommand cmd = reservationDAOConn.GetCommand(query);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                reservation["id"] = dataReader["ReservationId"].ToString() + "";
                reservation["productName"] = dataReader["ProductName"].ToString() + "";
                reservation["date"] = dataReader["ReservationDate"].ToString() + "";
                reservation["status"] = dataReader["ReservationStatus"].ToString() + "";
            }

            //close Data Reader
            dataReader.Close();

            reservationDAOConn.CloseConnection();
        }
        return reservation;
    }

    public int ReservationsCount()
    {
        return CommonQueries.Count(Constants.ReservationsTableName);
    }
}