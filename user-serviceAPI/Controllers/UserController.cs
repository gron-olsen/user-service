using Microsoft.AspNetCore.Mvc;
using userServiceAPI.Models;
using userServiceAPI.Services;
using MongoDB.Driver;
using System.Linq;

namespace userServiceAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
private readonly ILogger<UserController> _logger;
    public readonly IConfiguration _config;
    private readonly IUserRepository _userRepo;

    public UserController(ILogger<UserController> logger, IConfiguration configuration, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepo = userRepository;
        _config = configuration;
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

         [HttpPost("AddUser")]
    public IActionResult AddUser(User user)
    {
        _logger.LogInformation("Metode AddUser called at {DT}", DateTime.UtcNow.ToLongTimeString());
        _userRepo.AddUser(user);
        _logger.LogInformation("a new User with UserID {UserID} has been added.", user.UserID);
        return Ok();
    }
[HttpGet("GetUser")]
public async Task<IActionResult> GetUser([FromQuery] LoginModel loginModel)
{
    var user = await _userRepo.GetUser(loginModel);
    if (user == null)
    {
        return NotFound();
    }
    return Ok(user);
}
[HttpGet("GetUserById/{id}")]
     public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepo.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

    [HttpGet("getAllUsers")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {

        _logger.LogInformation("Metode GetAllUser called at {DT}", DateTime.UtcNow.ToLongTimeString());

        var list = await _userRepo.GetAllUsers();
        return Ok(list);
    }
    [HttpPut("UpdateUser")]
public async Task<IActionResult> UpdateUser(int UserID,User user)
    {
        _logger.LogInformation("Metode UpdateUser called at {DT}", DateTime.UtcNow.ToLongTimeString());
     //finds the User
     var exists = await _userRepo.GetUserById(UserID);
         if (exists == null)
     {    
        return NotFound();
    }
    //updates the User
    await _userRepo.UpdateUser(UserID, user);
        return Ok();
    }

    [HttpDelete("deleteUser/{id}")]
    public IActionResult DeleteUser(int id)
    {
        _logger.LogInformation("Metode DeleteUser called {DT}", DateTime.UtcNow.ToLongTimeString());

        _userRepo.DeleteUser(id);
        return Ok();
    }
}
