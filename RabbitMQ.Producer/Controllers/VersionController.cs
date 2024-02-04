using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RabbitMQ.Producer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VersionController : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve API version")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public string Get()
    {
        var version = Assembly.GetEntryAssembly()?.GetName().Version;
        return version!.ToString();
    }
}