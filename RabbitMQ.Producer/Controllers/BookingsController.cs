using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Producer.Domain.Dto;
using RabbitMQ.SharedProject;
using Swashbuckle.AspNetCore.Annotations;

namespace RabbitMQ.Producer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public BookingsController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a booking")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateBooking(BookingDto bookingDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        await _publishEndpoint.Publish<Booking>(new
        {
            Id = Guid.NewGuid(),
            Description = bookingDto.Description,
            Amount = bookingDto.Amount,
            Currency = bookingDto.Currency,
            CreatedAt = DateTime.UtcNow
        });

        return Ok();
    }
}