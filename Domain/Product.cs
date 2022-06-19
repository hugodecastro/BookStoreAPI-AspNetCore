using System.Diagnostics.CodeAnalysis;
using BookStore.DAO;

namespace BookStore.Domain;

public class Product
{
    public int ProductId { get; set; }
    public string? Name { get; set; }

    [return: MaybeNull]
    public static IProductDAO<Product> getProductDAOFactory(string ProductCategory)
    {
        if(ProductCategory == "Book")
            return new BookDAO();
        if(ProductCategory == "AudioBook")
            return new AudioBookDAO();
        return null;
    }
}
