using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;
using BankingApp;

namespace UVUBankUI.Views;

public partial class AccountCreation : Window
{
    private AccountBank bank;
    private IAccount account;
    public AccountCreation(AccountBank bank)
    {
        InitializeComponent();
        this.bank = bank;
    }

    private void DepositWithdraw_Click(object sender, RoutedEventArgs e)
    {
        DepositWithdraw transaction = new DepositWithdraw(bank);
        transaction.Show();
        this.Close();
    }

    private async void CreateSavingsAccount_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string amountInput = amountTextBox.Text;
        string nameInput = nameTextBox.Text;
        string addressInput = addressTextBox.Text;

        // Check if any of the required fields are empty
        if (string.IsNullOrWhiteSpace(nameInput) || string.IsNullOrWhiteSpace(addressInput) || string.IsNullOrWhiteSpace(amountInput))
        {
            ShowErrorMessage("Please fill in all required fields.");
            return;
        }

        // Create the savings account
        account = new SavingsAccount(amountInput);
        if (account.GetBalance() == 0)
        {
            ShowErrorMessage("New Savings Accounts Must Have $100 Initial Balance");
            return;
        }

        // Validate name and address
        if (!account.SetName(nameInput))
        {
            ShowErrorMessage("Enter a Valid Name");
            return;
        }

        if (!account.SetAddress(addressInput))
        {
            ShowErrorMessage("Enter a Valid Address");
            return;
        }

        ShowSuccessfulMessage("Savings Account Created\n", account);
    }

    private async void CreateCheckingAccount_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string amountInput = amountTextBox.Text;
        string nameInput = nameTextBox.Text;
        string addressInput = addressTextBox.Text;

        // Check if any of the required fields are empty
        if (string.IsNullOrWhiteSpace(nameInput) || string.IsNullOrWhiteSpace(addressInput) || string.IsNullOrWhiteSpace(amountInput))
        {
            ShowErrorMessage("Please fill in all required fields.");
            return;
        }

        // Create the checking account
        account = new CheckingAccount(amountInput);
        if (account.GetBalance() == 0)
        {
            ShowErrorMessage("New Checking Accounts Must Have $100 Initial Balance");
            return;
        }

        // Validate name and address
        if (!account.SetName(nameInput))
        {
            ShowErrorMessage("Enter a Valid Name");
            return;
        }

        if (!account.SetAddress(addressInput))
        {
            ShowErrorMessage("Enter a Valid Address");
            return;
        }

        // Show success message if account creation was successful
        ShowSuccessfulMessage("Checking Account Created", account);
    }

    private async void CreateCDAccount_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string amountInput = amountTextBox.Text;
        string nameInput = nameTextBox.Text;
        string addressInput = addressTextBox.Text;

        // Check if any of the required fields are empty
        if (string.IsNullOrWhiteSpace(nameInput) || string.IsNullOrWhiteSpace(addressInput) || string.IsNullOrWhiteSpace(amountInput))
        {
            ShowErrorMessage("Please fill in all required fields.");
            return;
        }

        // Create the CD account
        account = new CDAccount(amountInput);
        if (account.GetBalance() == 0)
        {
            
            ShowErrorMessage("New CD Accounts Must Have $500 Initial Balance");
            return;
        }

        // Validate name and address
        if (!account.SetName(nameInput))
        {
            ShowErrorMessage("Enter a Valid Name");
            return;
        }

        if (!account.SetAddress(addressInput))
        {
            ShowErrorMessage("Enter a Valid Address");
            return;
        }

        // Show success message if account creation was successful
        ShowSuccessfulMessage("CD Account Created", account);
    }


    private async void ShowSuccessfulMessage(string message, IAccount account)
    {
        if (bank.StoreAccount(account, account.GetAccountNumber()))
        {
            MessageBox.Text = message;
            MessageBox.IsVisible = true;
        }
        

    }
    
    private async void ShowErrorMessage(string message)
    {
        // Show error message
        MessageBox.Text = message;
        MessageBox.IsVisible = true;
    }
    
}