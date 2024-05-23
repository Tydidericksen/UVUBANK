// Project Prolog
// Name: Ty Didericksen
// CS3260 Section X01
// Project: Lab_03
// Date: 2/9/2024 9:30:00 AM
// Purpose : The purpose of this project is;
// to create a banking app that allows the user to create an account,
// deposit and withdraw funds, and display the account details.
// It also demonstrates my understanding of classes and interfaces.
//
// I declare that the following code was written by me or provided
// by the instructor for this project. I understand that copying source
// code from any other source constitutes plagiarism, and that I will receive
// a zero on this project if I am found in violation of this policy.


using System; 
using BankingApp;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTestUVUBank")]
[assembly: InternalsVisibleTo("UVUBankUI")]

// <summary>
// The main entry point for the application
// </summary>
class Program
{
   // <summary>
    // Counts the number of decimal places in a decimal value
    // </summary>
    // <param name="value">The decimal value to count the decimal places of</param>
    // <param name="maxDecimalPlaces">The maximum number of decimal places allowed</param>
    // <returns>True if the number of decimal places is less than or equal to the max, false if not</returns>
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


    // <summary>
    // Processes the user's action
    // </summary>
    // <param name="account">The account to process the action for</param>
    // <param name="actionInput">The user's input for the action</param>
    // <returns>True if the action was successful, false if not</returns>
    static bool ProcessAction(IAccount account, string actionInput)
    {
        // If the user wants to deposit
        if (actionInput == "1")
        {
            return ProcessDeposit(account);
        }
        // If the user wants to withdraw
        else if (actionInput == "2")
        {
            return ProcessWithdrawal(account);
        }
        else
        {
            return false; // Indicate an invalid action
        }
    }

    // <summary>
    // Processes the deposit action
    // </summary>
    // <param name="account">The account to process the deposit for</param>
    // <returns>True if the deposit was successful, false if not</returns>
    static bool ProcessDeposit(IAccount account)
    {
        // Prompt the user for the amount
        while (true)
        {
            // check to make sure the account isn't a CD account
            if (account is CDAccount)
            {
                Console.WriteLine("You cannot deposit funds into a CD account.");
                return false; // Indicate unsuccessful deposit
            }

            Console.WriteLine("Enter the amount to deposit (or type 'q' to go back): ");
            string amountInput = Console.ReadLine();

            // If the user wants to go back
            if (amountInput == "q")
            {
                return false; // Indicate unsuccessful deposit
            }

            // Try to parse the amount
            if (decimal.TryParse(amountInput, out decimal amount))
            {
                // check if the amount is greater than 0
                if (amount > 0)
                {
                    // check if the amount has 2 or fewer decimal places
                    if (!CountDecimalPlaces(amount, 2))
                    {
                        // Display an error message if the amount input is invalid
                        Console.WriteLine("Invalid response. Please enter a value with 2 or fewer decimal places.");
                        continue;
                    }

                    // Try to deposit the amount
                    if (account.PayInFunds(amount))
                    {
                        // Display the new balance
                        Console.WriteLine("Deposit successful. New balance: " + account.GetBalance());
                        return true; // Indicate successful deposit
                    }
                    else
                    {
                        // Display an error message if the deposit fails
                        Console.WriteLine("Deposit failed. Please try again.");
                    }
                }
                else
                {
                    // Display an error message if the amount input is invalid
                    Console.WriteLine("Invalid response. Please enter a value greater than 0.");
                }
            }
            else
            {
                // Display an error message if the amount input is invalid
                Console.WriteLine("Invalid response. Please try again.");
            }
        }
    }

    // <summary>
    // Processes the withdrawal action
    // </summary>
    // <param name="account">The account to process the withdrawal for</param>
    // <returns>True if the withdrawal was successful, false if not</returns>
    static bool ProcessWithdrawal(IAccount account)
    {
        // Prompt the user for the amount
        while (true)
        {
            Console.WriteLine("Enter the amount to withdraw (or type 'q' go back): ");
            string amountInput = Console.ReadLine();

            // If the user wants to go back
            if (amountInput == "q")
            {
                return false; // Indicate unsuccessful withdrawal
            }

            // Try to parse the amount
            if (decimal.TryParse(amountInput, out decimal amount))
            {
                // check if the amount is greater than 0
                if (amount > 0)
                {
                    // check if the amount has 2 or fewer decimal places
                    if (!CountDecimalPlaces(amount, 2))
                    {
                        // Display an error message if the amount input is invalid
                        Console.WriteLine("Invalid response. Please enter a value with 2 or fewer decimal places.");
                        continue;
                    }

                    // Try to withdraw the amount
                    if (account.WithdrawFunds(amount))
                    {
                        // Display the new balance
                        Console.WriteLine("Withdrawal successful. New balance: " + account.GetBalance());
                        return true; // Indicate successful withdrawal
                    }
                    else
                    {
                        // Display an error message if the withdrawal fails
                        Console.WriteLine("Withdrawal failed. Please try again.");
                    }
                }
                else
                {
                    // Display an error message if the amount input is invalid
                    Console.WriteLine("Invalid response. Please enter a value greater than 0.");
                }
            }
            else
            {
                // Display an error message if the amount input is invalid
                Console.WriteLine("Invalid response. Please try again.");
            }
        }
    }

    // <summary>
    // The account creation mode for the application
    // </summary>
    // <param name="bank">The bank to store the accounts in</param>
    // <returns>True if the account was created successfully, false if not</returns>
    static void AccountCreationMode(AccountBank bank)
    {

        while(true)
        {
            // ask for what type of account to create
            Console.WriteLine("Enter the type of account you would like to create: 1-Checking, 2-Savings, 3-CD. Or type 'done' when finished.");
            string accountTypeInput = Console.ReadLine();
            
            if (accountTypeInput == "done")
            {
                break;
            }
            // Create a new instance of the Account class based on the user's input
            IAccount account;

            if (accountTypeInput == "1")
            {
                Console.WriteLine("How much would you like to deposit into your checking account?");
                string amountInput = Console.ReadLine();
                account = new CheckingAccount(amountInput);
                if (account.GetBalance() == 0)
                {
                    Console.WriteLine("You must deposit at least $100 to open a checking account.");
                    
                    continue;
                }
                else
                {
                    Console.WriteLine("Your checking account has been created.");
                }
            }
            else if (accountTypeInput == "2")
            {
                Console.WriteLine("How much would you like to start your savings account with?");
                string amountInput = Console.ReadLine();
                account = new SavingsAccount(amountInput);
                if (account.GetBalance() == 0)
                {
                    Console.WriteLine("You must deposit at least $100 to open a savings account.");
                    
                    continue;
                }
                else
                {
                    Console.WriteLine("Your savings account has been created.");
                }
            }
            else if (accountTypeInput == "3")
            {
                Console.WriteLine("How much would you like to start your CD account with?");
                string amountInput = Console.ReadLine();
                account = new CDAccount(amountInput);
                if (account.GetBalance() == 0)
                {
                    Console.WriteLine("You must deposit at least $500 to open a CD account.");
                    
                    continue;
                }
                else
                {
                    Console.WriteLine("Your CD account has been created.");
                }
            }
            else
            {
                Console.WriteLine("Invalid response. Please try again.");
                
                continue;
            }

        
            // Prompt the user for name input
            while (true)
            {
                Console.WriteLine("Enter the name for the account: ");
                string nameInput = Console.ReadLine();

                // Try to set the name using the SetName method
                if (account.SetName(nameInput))
                {
                    // Break out of the loop if the name is successfully set
                    break;
                }
                else
                {
                    // Display an error message if the name input is invalid
                    Console.WriteLine("Invalid response. Please try again.");
                }
            }

            // Prompt the user for address input
            while (true)
            {
                Console.Write("Enter the address for the account: ");
                string addressInput = Console.ReadLine();

                // Try to set the address using the SetAddress method
                if (account.SetAddress(addressInput))
                {
                    // Break out of the loop if the address is successfully set
                    break;
                }
                else
                {
                    // Display an error message if the address input is invalid
                    Console.WriteLine("Invalid response. Please try again.");
                }
            }

            // Store the account in the bank
            if (bank.StoreAccount(account, account.GetAccountNumber()))
            {
                Console.WriteLine("Account created successfully. Details:\n" + account.ToString());
            }
            else
            {
                Console.WriteLine("Account creation failed. Please try again.");
            }

        }

        // display the accounts stored in the bank
        Console.WriteLine("Here are the accounts that have been created: ");
        foreach (IAccount account in bank.GetAllAccounts().Values)
        {
            if (account != null)
            {
                Console.WriteLine(account.ToString());
                Console.WriteLine("______________________________");
            }
        }

    }
    

    // <summary>
    // The main entry point for the application. This is where most of the application logic is handled. 
    // It uses the AccountBank class to store and manage the accounts. While the user is in the account selection mode,
    // they can select an account to perform actions on, such as depositing and withdrawing funds.
    // </summary>
    static void Main()
    {
        while (true)
        {
            // Welcome message
            Console.WriteLine("Welcome to the Banking App!");
            // Create a new instance of the AccountBank class
            AccountBank bank = new AccountBank();

            // Call the AccountCreationMode method
            AccountCreationMode(bank);

            // Account Selection Mode
            while (true)
            {   
                Console.WriteLine("Account Selection Mode:");
                Console.WriteLine("\nThese are the accountnumbers you can pick from: ");
                foreach (string accountNumber in bank.GetAllAccounts().Keys)
                {
                    Console.WriteLine(accountNumber + "-" + bank.FindAccount(accountNumber).GetName());
                }
                Console.WriteLine("Enter the account number to select an account.");
                Console.WriteLine("You can also type 'exit' to exit the program. Or type 'create' to create more accounts.");
                string accountNumberInput = Console.ReadLine();

                if (accountNumberInput == "create")
                {
                    AccountCreationMode(bank);
                    continue;
                }

                if (accountNumberInput == "exit")
                {
                    break;
                }

                IAccount selectedAccount = bank.FindAccount(accountNumberInput);

                if (selectedAccount != null)
                {
                    Console.WriteLine("Account selected: " + selectedAccount.ToString());
                    Console.WriteLine("Account Processing Mode:");
                    while (true)
                    {
                        Console.WriteLine("Enter the desired action: 1-Deposit, 2-Withdraw, 3-Return to account selection mode.");
                        string actionInput = Console.ReadLine();
                        if (actionInput == "3")
                        {
                            break;
                        }

                        // Call the ProcessAction method to handle the user's action
                        if (!ProcessAction(selectedAccount, actionInput))
                        {
                            // If ProcessAction returns false, the action was not successful
                            Console.WriteLine("Invalid response. Please try again.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Account not found. Please try again.");
                }
            }
            // Exit message
            Console.WriteLine("Thank you for banking today. Now exiting program...");
            break;
        }
    }

    
}