using userServiceAPI.Models;


namespace userServiceAPI.Services
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUser(int UserID);
        Task<List<User>> GetAllUsers();
        Task UpdateUser(int UserID, User user);
        Task DeleteUser(int UserID);
    }
}