using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore;

public class EfCoreUserRepository : EfCoreGenericRepository<User, ShopContext>, IUserRepository
{
    public User? GetUserById(int id)
    {
        using (var context = new ShopContext())
        {
            return context.Users
                .FirstOrDefault(i => i.Id == id);
        }
    }

   
    public async Task<User> Login(string username, string password)
    {
        using (var context = new ShopContext())
        {
            return await context.Users
                .SingleOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
    }

    public async Task<(bool Success, string ErrorMessage)> Register(string username, string password, string name, string surname)
    {
        User user = await Login(username, password);

        if (user != null && user.Username.Length > 1)
        {
            return (false, "Username already exists.");
        }

         using (var context = new ShopContext())
        {
            User newUser = new User
            {
                Username = username,
                Password = password,
                Name = name,
                Surname = surname
            };
           context.Users.Add(newUser);
           context.SaveChanges();
            return (true, string.Empty);
        }
    }


}