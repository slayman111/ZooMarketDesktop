using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZooMarketDesktop.DbService.Exception;
using ZooMarketDesktop.Model.Entity;
using ZooMarketDesktop.Model.Entity.Context;

namespace ZooMarketDesktop.DbService;

internal class AuthorizationService
{
    public static async Task<User> Login(string login, string password)
    {
        await using var context = new ZooMarketDbContext();

        var user = await context.Users.FirstOrDefaultAsync(u =>
            u.Login.Equals(login) && u.Password.Equals(password));

        if (user is null) throw new UserNotFoundException();

        return user;
    }
}
