using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ZooMarketDesktop.DbService;

namespace ZooMarketDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
#if DEBUG
            Task.Run(async () => await DbStartupService.EnsureCreatedAsync());
#endif

            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
