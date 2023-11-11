using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZooMarketDesktop.DbService.Exception;
using ZooMarketDesktop.Model.Entity;
using ZooMarketDesktop.Model.Entity.Context;

namespace ZooMarketDesktop.DbService;

internal static class AuthorizationService
{
    public static async Task<User> LoginAsync(string? login, string? password)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            throw new EmptyCredentialsException();

        await using var context = new ZooMarketDbContext();

        var user = await context.Users.Include(u => u.Role).FirstOrDefaultAsync(u =>
            u.Login.Equals(login) && u.Password.Equals(password));

        if (user is null) throw new UserNotFoundException();

        return user;
    }
}
