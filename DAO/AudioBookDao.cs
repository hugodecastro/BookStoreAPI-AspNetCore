using BookStore.Domain;

namespace BookStore.DAO;

public class AudioBookDAO : IProductDAO<Product>
{
    public void Insert(int productCategoryMapId, string[] args)
    {
        /// <summary>
        /// Insert new audio book.
        /// </summary>
        /// <param name="args">List of fields to be inserted.</param>
        /// <returns>This method returns nothing.</returns>

        throw new NotImplementedException();
    }

    public void UpdateByName(List<string> args, string name)
    {
        /// <summary>
        /// Update audio book by name.
        /// </summary>
        /// <param name="args">List of fields to be updated.</param>
        /// <param name="name">Name of the audio book to be updated.</param>
        /// <returns>This method returns nothing.</returns>
        /// <raises>MissingFieldException</raises>

        throw new NotImplementedException();
    }

    public void Delete(string name)
    {
        /// <summary>
        /// Delete book by name.
        /// </summary>
        /// <param name="name">Name of the audio book to be deleted.</param>
        /// <returns>This method returns nothing.</returns>

        throw new NotImplementedException();
    }

    public List<Product> SelectAllProductsInfo()
    {
        /// <summary>
        /// Select all audio books.
        /// </summary>
        /// <returns>List of audio books.</returns>

        throw new NotImplementedException();
    }

    public Product SelectProductInfoByName(string name)
    {
        /// <summary>
        /// Select audio book by name.
        /// </summary>
        /// <param name="name">Name of the audio book to be selected.</param>
        /// <returns>Audio Book</returns>

        throw new NotImplementedException();
    }

    public int ProductCount()
    {
        /// <summary>
        /// Count all audio books.
        /// </summary>
        /// <returns>Total amount of audio books.</returns>

        throw new NotImplementedException();
    }
}
