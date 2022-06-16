namespace BookStore.DAO;

public interface IProductDAO<T>
{
    void Insert(int productCategoryMapId, string[] args);
    void UpdateByName(List<string> args, string name);
    void Delete(string name);
    T SelectProductInfoByName(string name);
    List<T> SelectAllProductsInfo();
    int ProductCount();
}
