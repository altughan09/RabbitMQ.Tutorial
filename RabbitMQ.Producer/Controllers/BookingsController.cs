using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Producer.Domain.Constants;
using RabbitMQ.Producer.Domain.Dto;
using RabbitMQ.Producer.Domain.Entities;
using RabbitMQ.Producer.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RabbitMQ.Producer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IMessageProducer _messageProducer;

    public BookingsController(IMessageProducer messageProducer)
    {
        _messageProducer = messageProducer;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a booking")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateBooking(BookingDto bookingDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        // Logic to create a booking
        var booking = new Booking
        {
            Description = bookingDto.Description,
            Amount = bookingDto.Amount,
            Currency = bookingDto.Currency
        };
        
        _messageProducer.SendMessage(booking, Queue.BOOKINGS);

        return Ok();
    }
}