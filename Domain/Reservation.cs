namespace BookStore.Domain;

public class Reservation
{
    public int ReservationId { get; set; }

    public int ProductId { get; set; }

    public string? Date{ get; set; }

    public bool Status { get; set; }

    public override string ToString()
   {
      return "Reserva de id=" + ReservationId + " do livro de id=" + ProductId + " foi feita no dia=" + Date + "\ne está com status=" + Status;
   }
}
