using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Serilog;
using BookStore.Domain;

namespace BookStore.DAO;

public class BookDAO : IProductDAO<Book>
{
    private const string Tablename = "Books";
    private List<Book> booksList = new List<Book>();
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

        if (bookDAOConn.OpenConnection())
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
        /// <summary>
        /// Delete book by name.
        /// </summary>
        /// <param name="name">Name of the book to be deleted.</param>
        /// <returns>This method returns nothing.</returns>

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
    public List<Book> SelectAll()
    {
        /// <summary>
        /// Select all books.
        /// </summary>
        /// <returns>List of books.</returns>

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
                Book book = new Book();
                book.Id = (int) dataReader["id"];
                book.Name = dataReader["name"].ToString() + "";
                book.Year = dataReader["year"].ToString() + "";
                book.Author = dataReader["author"].ToString() + "";
                booksList.Add(book);
            }

            //close Data Reader
            dataReader.Close();

            bookDAOConn.CloseConnection();
            
        }
        
        return booksList;
    }

    public Book SelectByName(string name)
    {
        /// <summary>
        /// Select book by name.
        /// </summary>
        /// <param name="name">Name of the book to be selected.</param>
        /// <returns>Book</returns>

        Book book = new Book();
        bookDAOConn = new DbConnection();
        string query = $"SELECT * FROM {Tablename} WHERE name='{name}'";

        if (bookDAOConn.OpenConnection() == true)
        {
            MySqlCommand cmd = bookDAOConn.GetCommand(query);

            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                book.Id = (int) dataReader["id"];
                book.Name = dataReader["name"].ToString() + "";
                book.Year = dataReader["year"].ToString() + "";
                book.Author = dataReader["author"].ToString() + "";
            }

            dataReader.Close();
            bookDAOConn.CloseConnection();
        }
        return book;
    }

    //Count statement
    public int Count()
    {
        /// <summary>
        /// Count all books.
        /// </summary>
        /// <returns>Total amount of books.</returns>

        string query = $"SELECT COUNT(*) FROM {Tablename}";
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
