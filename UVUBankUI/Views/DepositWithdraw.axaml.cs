using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;
using System.Collections.Generic;
using Avalonia.Controls.Primitives;
using BankingApp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace UVUBankUI.Views;

public partial class DepositWithdraw : Window
{
    private AccountBank bank;
    public DepositWithdraw(AccountBank bank)
    {
        InitializeComponent();
        this.bank = bank;
        PopulateComboBox(GetAccounts());
    }
    
    static bool CountDecimalPlaces(decimal value, int maxDecimalPlaces)
    {
        // Convert the decimal to a string
        string valueString = value.ToString();

        // Find the index of the decimal point
        int decimalIndex = valueString.IndexOf('.');

        // If there is no decimal point, return true
        if (decimalIndex == -1)
        {
            return true;
        }

        // Get the number of decimal places
        int decimalPlaces = valueString.Substring(decimalIndex + 1).Length;

        // If the number of decimal places is greater than the max, return false
        if (decimalPlaces > maxDecimalPlaces)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        AccountCreation mainWindow = new AccountCreation(bank);
        mainWindow.Show();
        this.Close();
        
    }

    private void PopulateComboBox(List<string> accounts)
    {
        accountComboBox.Items.Clear();

        foreach (string account in accounts)
        {
            accountComboBox.Items.Add(account);
        }
    }

    private List<string> GetAccounts()
    {
        List<string> accountStrings = new List<string>();

        Dictionary<string, IAccount> allAccounts = bank.GetAllAccounts();

        foreach (var kvp in allAccounts)
        {
            IAccount account = kvp.Value;
            string accountString = account.ToString();
            accountStrings.Add(accountString);
        }

        return accountStrings;
    }

    
private async void Withdraw_Click(object sender, RoutedEventArgs e)
{
    string amountInput = amountTextBox.Text;

    // Check if amount is provided
    if (string.IsNullOrWhiteSpace(amountInput))
    {
        ShowErrorMessage("Please provide the amount.");
        return;
    }

    // Check if amount is a valid number
    if (!decimal.TryParse(amountInput, out decimal newAmount))
    {
        ShowErrorMessage($"{amountInput} is an invalid amount. Please enter a valid number.");
        return;
    }

    string selectedAccountInfo = accountComboBox.SelectedItem as string;
    if (string.IsNullOrWhiteSpace(selectedAccountInfo))
    {
        ShowErrorMessage("Please select an account.");
        return;
    }

    string selectedAccountNumber = selectedAccountInfo.Substring(selectedAccountInfo.Length - 5);
    if (string.IsNullOrWhiteSpace(selectedAccountNumber))
    {
        ShowErrorMessage("Invalid account number.");
        return;
    }

    IAccount account = bank.FindAccount(selectedAccountNumber);
    if (account == null)
    {
        ShowErrorMessage("Account not found");
        return;
    }
    

    if (newAmount <= 0)
    {
        ShowErrorMessage("Invalid Amount. Please enter a value greater than 0.");
        return;
    }

    if (!CountDecimalPlaces(newAmount, 2))
    {
        ShowErrorMessage("Invalid Amount! Please enter a value with 2 or fewer decimal places");
        return;
    }

    if (account.WithdrawFunds(newAmount))
    {
        ShowSuccessfulMessage($"Withdraw successful. New Balance: {account.GetBalance()}");
    }
    else
    {
        ShowErrorMessage($"Withdraw failed. Balance: {account.GetBalance()}");
    }
}

private async void Deposit_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
{
    string amountInput = amountTextBox.Text;

    // Check if amount is provided
    if (string.IsNullOrWhiteSpace(amountInput))
    {
        ShowErrorMessage("Please provide the amount.");
        return;
    }

    // Check if amount is a valid number
    if (!decimal.TryParse(amountInput, out decimal newAmount))
    {
        ShowErrorMessage($"{amountInput} is an invalid amount. Please enter a valid number.");
        return;
    }

    string selectedAccountInfo = accountComboBox.SelectedItem as string;
    if (string.IsNullOrWhiteSpace(selectedAccountInfo))
    {
        ShowErrorMessage("Please select an account.");
        return;
    }

    string selectedAccountNumber = selectedAccountInfo.Substring(selectedAccountInfo.Length - 5);
    Console.WriteLine(selectedAccountNumber);
    if (string.IsNullOrWhiteSpace(selectedAccountNumber))
    {
        ShowErrorMessage("Invalid account number.");
        return;
    }

    IAccount account = bank.FindAccount(selectedAccountNumber);
    if (account == null)
    {
        ShowErrorMessage("Account not found");
        return;
    }
    if (account is CDAccount)
    {
        ShowErrorMessage("Cannot Deposit into a CD account ");
    }

    if (!CountDecimalPlaces(newAmount, 2))
    {
        ShowErrorMessage("Invalid Amount! Please enter a value with 2 or fewer decimal places");
        return;
    }

    if (account.PayInFunds(newAmount))
    {
        ShowSuccessfulMessage($"Deposit successful. New Balance: {account.GetBalance()}");
    }
    else
    {
        ShowErrorMessage($"Deposit failed. Balance: {account.GetBalance()}");
    }
}

    
    private void ShowSuccessfulMessage(string message)
    {
            MessageBox.Text = message;
            MessageBox.IsVisible = true;
    }
    
    private void ShowErrorMessage(string message)
    {
        // Show error message
        MessageBox.Text = message;
        MessageBox.IsVisible = true;
    }


}