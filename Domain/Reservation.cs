namespace BookStore.Domain;

public class Reservation
{
    public int id { get; set; }

    public int bookId { get; set; }

    public string? date{ get; set; }

    public override string ToString()
   {
      return "Reserva de id=" + id + " do livro de id=" + bookId + " foi feita no dia=" + date;
   }
}
