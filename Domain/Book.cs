namespace BookStore.Domain;

public class Book : IProduct
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Year{ get; set; }

    public string? Author { get; set; }

    public string? Type { get; set; }

    public override string ToString()
   {
      return "Livro de id=" + Id + " de nome=" + Name + "\ne autor=" + Author + " foi publicado no ano=" + Year;
   }
}
