using Microsoft.AspNetCore.Mvc;
using userserviceAPI.Models;

namespace userserviceAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserServiceController : ControllerBase
{
    private readonly ILogger<UserServiceController> _logger;

    public UserServiceController(ILogger<UserServiceController> logger)
    {
        _logger = logger;
    }

    [HttpGet("version")]
     public IEnumerable<string> Get()
    {
        var properties = new List<string>();
        var assembly = typeof(Program).Assembly;
        foreach (var attribute in assembly.GetCustomAttributesData())
        {
            properties.Add($"{attribute.AttributeType.Name} - {attribute.ToString()}");
        }
        return properties;
    }

    [HttpGet("getUser")]
    {

    }
    [HttpPost("postUser")]
    {

    }
}
