using BookStore.DAO;
using MySql.Data.MySqlClient;

namespace BookStore.Util;

public class CommonQueries
{
    public static int Count(string tableName)
    {
        /// <summary>
        /// Count all reservations.
        /// </summary>
        /// <returns>Total amount of reservations.</returns>

        string query = $"SELECT COUNT(*) FROM {tableName};";
        int Count = -1;
        DbConnection conn = new DbConnection();

        //Open Connection
        if (conn.OpenConnection())
        {
            //Create Mysql Command
            MySqlCommand cmd = conn.GetCommand(query);

            //ExecuteScalar will return one value
            Count = int.Parse(cmd.ExecuteScalar()+"");
            
            //close Connection
            conn.CloseConnection();
        }

        return Count;
    }
}