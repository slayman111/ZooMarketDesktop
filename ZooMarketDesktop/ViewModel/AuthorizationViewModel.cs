using System.Windows.Input;
using ZooMarketDesktop.Command;
using ZooMarketDesktop.Core;
using ZooMarketDesktop.Core.Constant;
using ZooMarketDesktop.Core.Exception;
using ZooMarketDesktop.DbService;
using ZooMarketDesktop.View;

namespace ZooMarketDesktop.ViewModel;

internal class AuthorizationViewModel : BaseViewModel
{
    private string? _login;
    private string? _password;

    public string? Login { get => _login; set => SetProperty(ref _login, value); }
    public string? Password { get => _password; set => SetProperty(ref _password, value); }

    public ICommand SignInCommand { get; private set; }

    public AuthorizationViewModel()
    {
        SignInCommand = new DelegateCommand(SignIn);
    }

    private async void SignIn(object obj)
    {
        var user = await AuthorizationService.LoginAsync(Login, Password);

        if (user.Role.Name != RoleEnum.ADMIN.ToString()) throw new IncorrectAccessRightsException();

        CurrentUser.User = user;
        WindowManager.Open<DashboardWindow, AuthorizationWindow>();
    }
}
