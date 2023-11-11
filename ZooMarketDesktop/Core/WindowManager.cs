using System.Linq;
using System.Windows;

namespace ZooMarketDesktop.Core;

internal static class WindowManager
{
    public static void Open<TOpen, TClose>() where TOpen : Window, new()
    {
        new TOpen().Show();
        Application.Current?.Windows.Cast<Window>().FirstOrDefault(window => window is TClose)?.Close();
    }
}
