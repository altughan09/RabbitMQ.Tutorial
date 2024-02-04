namespace RabbitMQ.SharedProject
{
  public class Booking
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Description { get; set; }
    public double Amount { get; set; }
    public required string Currency { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  }
}