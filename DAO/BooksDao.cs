using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Serilog;
using BookStore.Domain;
using BookStore.Util;

namespace BookStore.DAO;

public class BookDAO : IProductDAO<Product>
{
    private List<Product> booksList = new List<Product>();
    // Instance responsible for dealing with DB Connections
    DbConnection? bookDAOConn;
    
    //Insert statement
    public void Insert(int productCategoryMapId, string[] args)
    {
        /// <summary>
        /// Insert new book.
        /// </summary>
        /// <param name="args">List of fields to be inserted.</param>
        /// <returns>This method returns nothing.</returns>

        bookDAOConn = new DbConnection();
        string query = $"INSERT INTO {Constants.BooksTableName} " + 
                        "(ProductCategoryMapId, YearPublished, Author) " +
                        $"VALUES({productCategoryMapId},";

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
        /// <raises>MissingFieldException</raises>

        //#TODO flexibilize numbers of arguments to update
        if(args.Count < 3)
        {
            throw new MissingFieldException("Missing value to update!");
        }
        bookDAOConn = new DbConnection();
        var regex = new Regex(Regex.Escape("0"));
        string query = $"UPDATE {Constants.BooksTableName} b" + 
                       $"LEFT JOIN {Constants.ProductCategoryMapTableName} pcm " +
                       "ON b.ProductCategoryMapId = pcm.ProductCategoryMapId " + 
                       $"RIGHT JOIN {Constants.ProductsTableName} p " +
                       "ON p.ProductId = pcm.ProductId " +
                       "SET p.ProductName='0', b.YearPublished='0', b.Author='0' " +
                       $"WHERE p.ProductName='{name}'";

        foreach(string field in args)
        {
            // Replace the first occurrence of 0 (zero) with the given field 
            query = regex.Replace(query, field, 1);
        }
 
        if (bookDAOConn.OpenConnection())
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

        string query = $"DELETE FROM {Constants.BooksTableName} " + 
                       $"LEFT JOIN {Constants.ProductCategoryMapTableName} pcm " +
                       "ON b.ProductCategoryMapId = pcm.ProductCategoryMapId " +
                       $"RIGHT JOIN {Constants.ProductsTableName} p " +
                       "ON p.ProductId = pcm.ProductId " +
                       $"WHERE p.ProductName='{name}'";
        bookDAOConn = new DbConnection();

        if (bookDAOConn.OpenConnection())
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
    public Product SelectProductInfoByName(string name)
    {
        /// <summary>
        /// Select book by name.
        /// </summary>
        /// <param name="name">Name of the book to be selected.</param>
        /// <returns>Book</returns>

        bookDAOConn = new DbConnection();
        Book book = new Book();
        string query = "SELECT p.ProductId, p.ProductName, b.YearPublished, b.Author " +
                       $"FROM {Constants.ProductsTableName} p " + 
                       $"LEFT JOIN {Constants.ProductCategoryMapTableName} pcm " + 
                       "ON p.ProductId = pcm.ProductCategoryMapId " + 
                       $"RIGHT JOIN {Constants.BooksTableName} b " + 
                       "ON b.ProductCategoryMapId = pcm.ProductCategoryMapId " +
                       $"RIGHT JOIN {Constants.CategoriesTableName} c " + 
                       "ON c.CategoryId = pcm.CategoryId " +
                       $"WHERE p.ProductName = '{name}' " + 
                       "AND c.CategoryId = 1;";

        if (bookDAOConn.OpenConnection())
        {
            //Create Command
            MySqlCommand cmd = bookDAOConn.GetCommand(query);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                book.ProductId = (int) dataReader["ProductId"];
                book.Name = dataReader["ProductName"].ToString() + "";
                book.Year = dataReader["YearPublished"].ToString() + "";
                book.Author = dataReader["Author"].ToString() + "";
            }

            //close Data Reader
            dataReader.Close();

            bookDAOConn.CloseConnection();
        }
        
        return book;
    }

    public List<Product> SelectAllProductsInfo()
    {
        /// <summary>
        /// Select all books.
        /// </summary>
        /// <returns>List of books.</returns>

        bookDAOConn = new DbConnection();
        string query = "SELECT p.ProductId, p.ProductName, b.YearPublished, b.Author " +
                       $"FROM {Constants.ProductsTableName} p " + 
                       $"LEFT JOIN {Constants.ProductCategoryMapTableName} pcm " + 
                       "ON p.ProductId = pcm.ProductCategoryMapId " + 
                       $"RIGHT JOIN {Constants.BooksTableName} b " + 
                       "ON b.ProductCategoryMapId = pcm.ProductCategoryMapId " +
                       $"RIGHT JOIN {Constants.CategoriesTableName} c " + 
                       "ON c.CategoryId = pcm.CategoryId " +
                       "WHERE p.ProductName IS NOT NULL " +
                       "AND c.CategoryId = 1; ";

        if (bookDAOConn.OpenConnection())
        {
            //Create Command
            MySqlCommand cmd = bookDAOConn.GetCommand(query);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            
            //Read the data and store them in the list
            while (dataReader.Read())
            {
                Book book = new Book();
                book.ProductId = (int) dataReader["ProductId"];
                book.Name = dataReader["ProductName"].ToString() + "";
                book.Year = dataReader["YearPublished"].ToString() + "";
                book.Author = dataReader["Author"].ToString() + "";
                booksList.Add(book);
            }

            //close Data Reader
            dataReader.Close();

            bookDAOConn.CloseConnection();
        }
        
        return booksList;
    }

    public int ProductCount()
    {
        return CommonQueries.Count(Constants.BooksTableName);
    }
}
