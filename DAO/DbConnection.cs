using MySql.Data.MySqlClient;
using Serilog;

namespace BookStore.DAO;

public class DbConnection
{
    private const String ConnectionString = "Server=localhost;" + 
                                            "Database=Codurance;" + 
                                            "Port=3306;" + 
                                            "Uid=root;" + 
                                            "Pwd=A@dmin2345;";
    private MySqlConnection connection = new MySqlConnection(ConnectionString);

    //open connection to database
    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {   
            // _log.Error(ex.Message);
            Log.Error(ex.Message);
            return false;
        }
    }

    //Close connection
    public bool CloseConnection()
    {
        try
        {

            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Log.Error(ex.Message);
            return false;
        }
    }

    public MySqlCommand GetCommand(string query)
    {
        //create command and assign the query and connection from the constructor
        MySqlCommand cmd = new MySqlCommand(query, connection);

        return cmd;
    }
    public void ExecuteCommand(string query)
    {

        MySqlCommand cmd = this.GetCommand(query);

        //Execute query
        cmd.ExecuteNonQuery();
    }
}