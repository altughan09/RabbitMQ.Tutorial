using System.ComponentModel.DataAnnotations;

namespace RabbitMQ.Producer.Domain.Dto;

public class BookingDto
{
    public string? Description { get; set; }
    
    [Required]
    public double Amount { get; set; }
    
    [Required]
    public required string Currency { get; set; }
}