using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
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
    public ICommand DeleteProductCommand { get; private set; }

    public DashboardViewModel()
    {
        SignOutCommand = new DelegateCommand(SignOut);
        OpenNewProductWindowCommand = new DelegateCommand(OpenNewProductWindow);
        DeleteProductCommand = new DelegateCommand(DeleteProduct);

        User = CurrentUser.User;

        Task.Run(async () =>
        {
            await using var context = new ZooMarketDbContext();
            using var productService = new CrudDbService<int, Product>(context);

            Products = new ObservableCollection<Product>(await productService.GetAllAsync());
        });
    }

    private async void DeleteProduct(object id)
    {
        if (MessageBox.Show("Вы уверены что хотите удалить товар?", "Удаление", MessageBoxButton.YesNo) !=
            MessageBoxResult.Yes) return;

        await using var context = new ZooMarketDbContext();
        using var productService = new CrudDbService<int, Product>(context);

        await productService.DeleteAsync((int)id);

        Products = new ObservableCollection<Product>(await productService.GetAllAsync());
    }

    private static void SignOut(object obj)
    {
        CurrentUser.Reset();
        WindowManager.Open<AuthorizationWindow, DashboardWindow>();
    }

    private static void OpenNewProductWindow(object obj)
    {
        WindowManager.Open<NewProductWindow, DashboardWindow>();
    }
}
