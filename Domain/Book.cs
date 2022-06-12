namespace BookStore.Domain;

public class Book
{
    public int id { get; set; }

    public string? name { get; set; }

    public string? year{ get; set; }

    public string? author { get; set; }

    public override string ToString()
   {
      return "Livro de id=" + id + " de nome=" + name + "\ne autor=" + author + " foi publicado no ano=" + year;
   }
}
