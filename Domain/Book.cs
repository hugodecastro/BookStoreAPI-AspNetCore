namespace BookStore.Domain;

public class Book : Product
{
    public int BookId { get; set; }

    public string? Year{ get; set; }

    public string? Author { get; set; }


    public override string ToString()
   {
      return "Livro de id=" + BookId + " de nome=" + Name + "\ne autor=" + Author + " foi publicado no ano=" + Year;
   }
}
