using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace UVUBankUI.Views;

public partial class FindAccount : Window
{
    public FindAccount()
    {
        InitializeComponent();
    }
    
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        DepositWithdraw mainWindow = new DepositWithdraw();
        mainWindow.Show();
        this.Close();

    }
}