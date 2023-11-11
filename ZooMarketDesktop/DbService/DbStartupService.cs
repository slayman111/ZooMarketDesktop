using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ZooMarketDesktop.Core.Constant;
using ZooMarketDesktop.Model.Entity;
using ZooMarketDesktop.Model.Entity.Context;

namespace ZooMarketDesktop.DbService;

internal static class DbStartupService
{
    public static async Task EnsureCreatedAsync()
    {
        var context = new ZooMarketDbContext();
        if (await context.Database.EnsureCreatedAsync()) await InitTestData(context);

        var databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
        await databaseCreator.CreateTablesAsync();
    }

    private static async Task InitTestData(DbContext context)
    {
        var userService = new CrudDbService<int, User>(context);
        var roleService = new CrudDbService<int, Role>(context);

        var adminRole = await roleService.CreateAsync(new Role
        {
            Name = RoleEnum.ADMIN.ToString()
        });
        await roleService.CreateAsync(new Role
        {
            Name = RoleEnum.USER.ToString()
        });

        await userService.CreateAsync(new User
        {
            Firstname = "Admin",
            Lastname = "Admin",
            Patronymic = "Admin",
            Login = "admin",
            Password = "admin",
            Role = adminRole
        });
    }
}
