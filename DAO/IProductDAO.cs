namespace BookStore.DAO;

public interface IProductDAO<T>
{
    void Insert(string[] args);
    void UpdateByName(List<string> args, string name);
    void Delete(string name);
    List<T> SelectAll();
    T SelectByName(string name);
    int Count();
}