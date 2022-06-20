using BookStore.DAO;
using MySql.Data.MySqlClient;

namespace BookStore.Util;

public class CommonQueries
{
    public static string GetProductCategory(string ProductName, DbConnection conn)
    {
        string CategoryName = "";
        string query = "SELECT c.CategoryName " +
                       $"FROM {Constants.CategoriesTableName} c " + 
                       $"LEFT JOIN {Constants.ProductCategoryMapTableName} pcm " +
                       "ON c.CategoryId = pcm.CategoryId " +
                       $"RIGHT JOIN {Constants.ProductsTableName} p " +
                       "ON p.ProductId = pcm.ProductId " +
                       $"WHERE p.ProductName LIKE '%{ProductName}%';";
        
        if (conn.OpenConnection())
        {
            
            MySqlCommand cmd = conn.GetCommand(query);
         
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            
            CategoryName = dataReader["CategoryName"].ToString() + "";
            
            conn.CloseConnection();
        }

        return CategoryName;
    }

    public static int Count(string tableName, DbConnection conn)
    {
        /// <summary>
        /// Count all reservations.
        /// </summary>
        /// <returns>Total amount of reservations.</returns>

        string query = $"SELECT COUNT(*) FROM {tableName};";
        int Count = -1;

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