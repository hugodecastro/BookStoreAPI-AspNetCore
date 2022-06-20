using System.Diagnostics.CodeAnalysis;
using BookStore.Util;
using BookStore.Domain;

namespace BookStore.DAO;

public class ProductDaoFactory
{
    [return: MaybeNull]
    public static IProductDAO<Product> GetProductDAOFactory(string ProductName)
    {
        DbConnection conn = new DbConnection();
        string ProductCategory = CommonQueries.GetProductCategory(ProductName, conn);

        if(ProductCategory == "Book")
            return new BookDAO();
        if(ProductCategory == "AudioBook")
            return new AudioBookDAO();
        return null;
    }

    public static IProductDAO<Product> GetBookDAOFactory()
    {
        return new BookDAO();
    }
}