using EasyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyAPI.Repositories;

public class UserRepository(PostgresContext postgresContext) : RepositoryBase(postgresContext)
{
    public async Task<List<User>> GetUsers() => await Сontext.Users.ToListAsync();

    public async Task<User> GetUserById(int userid)
    {
        User? users = await Сontext.Users.FirstOrDefaultAsync(x => x.UserId == userid);
        return users ?? throw new Exception("users not found");
    }
    
    public async Task SaveChangesAsync() => await Сontext.SaveChangesAsync();

    public async Task DeleteUserById(int userid)
    {
        User? user = Сontext.Users.FirstOrDefault(x => x.UserId == userid);
        if (user != null)
        {
            Сontext.Users.Remove(user);
            await Сontext.SaveChangesAsync();
        }
    }

    public async Task UpdateUser(User user) 
    {
        try
        {
            Сontext.Users.Update(user);
            await Сontext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<User> AddUser(User user)
    {
        try
        {
            Сontext.Users.Add(user);
            await Сontext.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
