using System.Collections.Generic;
using System.Threading.Tasks;
using userServiceAPI.Models;
using MongoDB.Driver;
using userServiceAPI.Services;

namespace userServiceAPI.Services {

public class UserRepository : IUserRepository
{
    public readonly IConfiguration _configuration;
    private readonly IMongoCollection<User> _userCollection;

    public UserRepository(ILogger<UserRepository> logger, IConfiguration configuration)
    {
        
        _configuration = configuration;
    
        logger.LogInformation($"INFO: connection string is: {configuration["connectionString"]}");
    
        var mongoClient = new MongoClient(_configuration["connectionString"]);
        var database = mongoClient.GetDatabase(_configuration["database"]);
        _userCollection = database.GetCollection<User>(_configuration["collection"]);
    }
    

    public async Task AddUser(User user) =>
        await _userCollection.InsertOneAsync(user);

    
    public async Task<User> GetUser(LoginModel loginModel) =>

        await _userCollection.Find(u => u.UserName == loginModel.UserName).FirstOrDefaultAsync();

   public async Task<User> GetUserById(int id) =>

        await _userCollection.Find(u => u.UserID == id).FirstOrDefaultAsync();

    
    public async Task<List<User>> GetAllUsers() =>

        await _userCollection.Find(_ => true).ToListAsync();
    


    public async Task UpdateUser(int id, User user) =>

        await _userCollection.ReplaceOneAsync(u => u.UserID == id, user);


    public async Task DeleteUser(int id) =>
    
        await _userCollection.DeleteOneAsync(u => u.UserID == id);
}
}