using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Serilog;
using BookStore.Domain;
using System.Linq;

namespace BookStore.DAO;

public class BookDAO
{
    private const string Tablename = "Books";
    private Dictionary<string, string> bookFields = new Dictionary<string, string>();
    // Instance responsible for dealing with DB Connections
    DbConnection? bookDAOConn;
    
    //Insert statement
    public void Insert(string[] args)
    {
        /// <summary>
        /// Insert new book.
        /// </summary>
        /// <param name="args">List of fields to be updated.</param>
        /// <returns>This method returns nothing.</returns>

        bookDAOConn = new DbConnection();
        string query = $"INSERT INTO {Tablename} VALUES(";

        foreach(string field in args)
        {
            query += query + $"'{field}',"; 
        }
        query += query + ")";

        if (bookDAOConn.OpenConnection() == true)
        {
            bookDAOConn.ExecuteCommand(query);
            Log.Information("New book inserted successfully");
        }
        else
        {
            Log.Error($"Couldn't inset book {args[1]}");
        }
        bookDAOConn.CloseConnection();
        
    }
    

    //Update statement
    public void UpdateByName(List<string> args, string name)
    {
        /// <summary>
        /// Update book by name.
        /// </summary>
        /// <param name="args">List of fields to be updated.</param>
        /// <param name="name">Name of the book to be updated.</param>
        /// <returns>This method returns nothing.</returns>

        //#TODO flexibilize numbers of arguments to update
        if(args.Count < 3)
        {
            throw new Exception("Missing value to update!");
        }
        bookDAOConn = new DbConnection();
        var regex = new Regex(Regex.Escape("0"));
        string query = $"UPDATE {Tablename} SET name='0', year='0', author='0' WHERE name={name}";

        foreach(string field in args)
        {
            // Replace the first occurrence of 0 (zero) with the given field 
            query = regex.Replace(query, field, 1);
        }
 
        if (bookDAOConn.OpenConnection() == true)
        {
            bookDAOConn.ExecuteCommand(query);
            Log.Information("Book updated successfully!");
        }
        else
        {
            Log.Error($"Couldn't update book {name}");
        }
        bookDAOConn.CloseConnection();
    }

    //Delete statement
    public void Delete(string name)
    {
        string query = $"DELETE FROM {Tablename} WHERE name='{name}'";
        bookDAOConn = new DbConnection();

        if (bookDAOConn.OpenConnection() == true)
        {
            bookDAOConn.ExecuteCommand(query);
            Log.Information($"Book {name} deleted successfully!");
        }
        else
        {
            Log.Error($"Couldn't delete book {name}");
        }
        bookDAOConn.CloseConnection();
    }

    //Select statement
    public Dictionary<string, string> SelectAll()
    {
        string query = $"SELECT * FROM {Tablename}";
        bookDAOConn = new DbConnection();

        if (bookDAOConn.OpenConnection() == true)
        {
            //Create Command
            MySqlCommand cmd = bookDAOConn.GetCommand(query);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                bookFields["name"] = dataReader["name"].ToString() + "";
                bookFields["year"] = dataReader["year"].ToString() + "";
                bookFields["author"] = dataReader["author"].ToString() + "";
            }

            //close Data Reader
            dataReader.Close();

            bookDAOConn.CloseConnection();
            
        }
        
        return bookFields;
    }

    public Book SelectByName(string name)
    {
        Book book = new Book();
        bookDAOConn = new DbConnection();
        string query = $"SELECT * FROM {Tablename} WHERE name='{name}'";

        if (bookDAOConn.OpenConnection() == true)
        {
            MySqlCommand cmd = bookDAOConn.GetCommand(query);

            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                book.id = (int) dataReader["id"];
                book.name = dataReader["name"].ToString() + "";
                book.year = dataReader["year"].ToString() + "";
                book.author = dataReader["author"].ToString() + "";
            }

            dataReader.Close();
            bookDAOConn.CloseConnection();
        }
        return book;
    }

    //Count statement
    public int Count()
    {
        string query = "SELECT COUNT(*) FROM Books";
        int Count = -1;
        bookDAOConn = new DbConnection();

        //Open Connection
        if (bookDAOConn.OpenConnection() == true)
        {
            //Create Mysql Command
            MySqlCommand cmd = bookDAOConn.GetCommand(query);

            //ExecuteScalar will return one value
            Count = int.Parse(cmd.ExecuteScalar()+"");
            
            //close Connection
            bookDAOConn.CloseConnection();
        }

        return Count;
    }
}
