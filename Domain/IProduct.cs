namespace BookStore.Domain;

public interface IProduct
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
}