using System;
using System.Collections.Generic;

namespace BankingApp
{
    // <summary>
    // This class represents a bank that stores accounts
    // </summary>
    public class AccountBank
    {
        private Dictionary<string, IAccount> _accounts;

        // <summary>
        // Constructor for AccountBank
        // </summary>
        public AccountBank()
        {
            _accounts = new Dictionary<string, IAccount>();
        }

        // <summary>
        // Checks if the input is a valid number of accounts
        // </summary>
        // <param name="input">The input to check</param>
        // <returns>True if the input is a valid number of accounts, false otherwise</returns>
        public static bool IsNumberOfAccountsValid(string input)
        {
            if (int.TryParse(input, out int numAccounts))
            {
                return numAccounts > 0; // Ensure it's a positive whole integer
            }
            return false; // Parsing failed
        }

        // <summary>
        // Checks if the input is a valid account number
        // </summary>
        // <param name="input">The input to check</param>
        // <returns>True if the input is a valid account number, false otherwise</returns>
        public bool StoreAccount(IAccount newAccount, string accountNumber)
        {
            if (!_accounts.ContainsKey(accountNumber))
            {
                _accounts[accountNumber] = newAccount;
                return true;
            }
            else
            {
                return false;
            }
        }

        // <summary>
        // Finds an account in the bank by account number
        // </summary>
        // <param name="accountNumber">The account number to search for</param>
        // <returns>The account with the given account number, or null if no such account exists</returns>
        public IAccount FindAccount(string accountNumber)
        {
            if (_accounts.TryGetValue(accountNumber, out IAccount account))
            {
                return account;
            }
            return null;
        }

        // <summary>
        // Returns a Dictionary of all the accounts stored in the bank
        // </summary>
        // <returns>A Dictionary of all the accounts stored in the bank</returns>
        public Dictionary<string, IAccount> GetAllAccounts()
        {
            return _accounts;
        }
    }
}
