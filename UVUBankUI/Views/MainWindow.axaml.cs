using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;
using BankingApp;

namespace UVUBankUI.Views
{
    public partial class MainWindow : Window
    {
        AccountBank bank = new AccountBank();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountCreation create = new AccountCreation(bank);
            create.Show();
            this.Close();

        }
        
    }
}