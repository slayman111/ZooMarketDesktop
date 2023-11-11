using System.Windows;
using System.Windows.Controls;
using ZooMarketDesktop.ViewModel;

namespace ZooMarketDesktop.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class AuthorizationWindow
{
    public AuthorizationWindow()
    {
        InitializeComponent();
    }

    private void PasswordChanged(object sender, RoutedEventArgs e) =>
        (DataContext as AuthorizationViewModel)!.Password = ((PasswordBox)sender).Password;
}
