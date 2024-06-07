using shopapp.entity;

namespace shopapp.data.Abstract;

public interface IUserRepository : IRepository<User>
{
    User? GetUserById(int id);
    Task<User> Login(string username, string password);
    Task<(bool Success, string ErrorMessage)> Register(string username, string password, string name, string surname);
}
