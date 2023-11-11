using ZooMarketDesktop.Model.Entity;

namespace ZooMarketDesktop.Core;

internal static class CurrentUser
{
    public static User User { get; set; }

    static CurrentUser()
    {
        User = default!;
    }

    public static void Reset() => User = default!;
}
