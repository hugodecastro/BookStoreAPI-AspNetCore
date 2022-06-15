namespace BookStore.Domain;

public class Reservation
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string? Date{ get; set; }

    public override string ToString()
   {
      return "Reserva de id=" + Id + " do livro de id=" + ProductId + " foi feita no dia=" + Date;
   }
}
