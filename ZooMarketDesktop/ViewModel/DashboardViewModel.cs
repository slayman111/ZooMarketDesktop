using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ZooMarketDesktop.Command;
using ZooMarketDesktop.Core;
using ZooMarketDesktop.DbService;
using ZooMarketDesktop.Model.Entity;
using ZooMarketDesktop.Model.Entity.Context;
using ZooMarketDesktop.View;

namespace ZooMarketDesktop.ViewModel;

internal class DashboardViewModel : BaseViewModel
{
    private ObservableCollection<Product>? _products;

    public User User { get; }

    public ObservableCollection<Product>? Products
    {
        get => _products;
        private set => SetProperty(ref _products, value);
    }

    public ICommand SignOutCommand { get; private set; }
    public ICommand OpenNewProductWindowCommand { get; private set; }

    public DashboardViewModel()
    {
        User = CurrentUser.User;

        Task.Run(async () =>
        {
            await using var context = new ZooMarketDbContext();
            using var productService = new CrudDbService<int, Product>(context);

            Products = new ObservableCollection<Product>(await productService.GetAllAsync());
        });

        SignOutCommand = new DelegateCommand(SignOut);
        OpenNewProductWindowCommand = new DelegateCommand(OpenNewProductWindow);
    }

    private static void SignOut(object obj)
    {
        CurrentUser.Reset();
        WindowManager.Open<AuthorizationWindow, DashboardWindow>();
    }

    private static void OpenNewProductWindow(object obj)
    {
        CurrentUser.Reset();
        WindowManager.Open<NewProductWindow, DashboardWindow>();
    }
}
