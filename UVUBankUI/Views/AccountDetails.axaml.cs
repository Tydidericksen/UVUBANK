using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Threading.Tasks;

namespace UVUBankUI.Views;

public partial class AccountDetails : Window
{
    public AccountDetails()
    {
        InitializeComponent();
    }
    
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        AccountCreation previous = new AccountCreation();
        previous.Show();
        this.Close();

    }

    private async void CreateAccount_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Simulate account creation process (can be replaced with actual logic)
        await Task.Delay(1000);

        // Display account created message
        ShowAccountCreatedMessage();
    }

    private async void ShowAccountCreatedMessage()
    {
        accountCreatedMessage.IsVisible = true;

        // Wait for 2 seconds then hide the message
        await Task.Delay(2000);

        accountCreatedMessage.IsVisible = false;
    }
    
    
}