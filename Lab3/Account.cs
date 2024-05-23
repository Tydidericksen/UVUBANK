using System;

namespace BankingApp
{
    // <summary>
    // Enum for the state of the account
    // </summary>
    public enum AccountState
    {
        New,
        Active,
        UnderAudit,
        Frozen,
        Closed
    }
    // <summary>
    // This class represents the base account of all account types. It contains the basic account information and methods.
    // It is an abstract class and cannot be instantiated. An interface IAccount is implemented to ensure that all account types have the same methods.
    // </summary>
    public abstract class Account : IAccount
    {
        // Private fields
        private string _name;
        private string _address;
        private string _accountNumber;
        private static int _lastAccountNumber = 1000;
        private decimal _balance;
        private AccountState _state;

        // <summary>
        // Sets the name of the account holder
        // </summary>
        // <param name="inName">The name of the account holder</param>
        // <returns>True if the name is valid, false if not</returns>
        public bool SetName(string inName)
        {
            if (inName.Length <= 0)
            {
                return false;
            }
            else
            {
                _name = inName;
                return true;
            }
        }

        // <summary>
        // Gets the name of the account holder
        // </summary>
        // <returns>The name of the account holder</returns>
        public string GetName()
        {
            return _name;
        }

        // <summary>
        // Sets the address of the account holder
        // </summary>
        // <param name="inAddress">The address of the account holder</param>
        // <returns>True if the address is valid, false if not</returns>
        public bool SetAddress(string inAddress)
        {
            if (inAddress.Length <= 0)
            {
                return false;
            }
            else
            {
                _address = inAddress;
                return true;
            }
        }

        // <summary>
        // Gets the address of the account holder
        // </summary>
        // <returns>The address of the account holder</returns>
        public string GetAddress()
        {
            return _address;
        }
        // <summary>
        // Adds funds to the account
        // </summary>
        // <param name="amount">The amount to add to the account</param>
        // <returns>True if the amount is > 0, false if not</returns>
        public virtual bool PayInFunds(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        // <summary>
        // Withdraws funds from the account
        // </summary>
        // <param name="amount">The amount to withdraw from the account</param>
        // <returns>True if the amount is > 0 and the balance is > 0, false if not</returns>
        public virtual bool WithdrawFunds(decimal amount)
        {
            if (amount > 0)
            {
                _balance -= amount;
                // If the balance is less than 0 return false
                if (_balance < 0)
                {
                    _balance += amount;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        // <summary>
        // Sets the balance of the account
        // </summary>
        // <param name="amount">The amount to set the balance to</param>
        // <returns>True if the amount is >= 0, false if not</returns>
        public void SetBalance(decimal amount)
        {
            _balance = amount;
        }

        // <summary>
        // Gets the balance of the account
        // </summary>
        // <returns>The balance of the account</returns>
        public decimal GetBalance()
        {
            return Math.Round(_balance, 2);
        }

        // <summary>
        // Sets the state of the account
        // </summary>
        // <param name="state">The state to set the account to</param>
        public void SetState(AccountState state)
        {
            _state = state;
        }

        // <summary>
        // Gets the state of the account
        // </summary>
        // <returns>The state of the account</returns>
        public AccountState GetState()
        {
            return _state;
        }

        // <summary>
        // Sets the account number
        // </summary>
        // <param name="number">The account number to set</param>
        public void SetAccountNumber(string number)
        {
            _accountNumber = number;
        }

        // <summary>
        // Gets the account number
        // </summary>
        // <returns>The account number</returns>
        public string GetAccountNumber()
        {
            return _accountNumber;
        }

        // <summary>
        // Checks if the amountInput is a valid amount
        // </summary>
        // <param name="amountInput">The amount to check</param>
        // <returns>True if the amount is valid, false if not</returns>
        public bool IsAmountValid(string amountInput)
        {
            if (decimal.TryParse(amountInput, out decimal amount))
            {
                return amount > 0; // Ensure it's a positive number
            }
            return false; // Parsing failed
        }

        // <summary>
        // Constructor for the account class setting the initial balance, account state, and account number
        // </summary>
        public Account()
        {
            _balance = 0;
            _state = AccountState.New;
        }

        // <summary>
        // Overridden ToString method
        // </summary>
        // <returns>A string containing the account details</returns>
        public override string ToString()
        {
            return _name + " - " + _accountNumber;
        }
    }

    // <summary>
    // Child class of Account called SavingsAccount with a minimum balance of 100
    // </summary>
    internal class SavingsAccount : Account
    {
        private const decimal MinimumBalance = 100;
        private static int _lastAccountNumber = 1000;

        // <summary>
        // Constructor for the SavingsAccount class setting the initial balance, account state, and account number
        // </summary>
        // <param name="amountInput">The initial balance of the account</param>
        // <returns>True if the amount is valid, false if not</returns>
        public SavingsAccount(string amountInput)
        {
            if (decimal.TryParse(amountInput, out decimal initialBalance))
            {
                if (initialBalance >= MinimumBalance)
                {
                    SetBalance(initialBalance);
                }
                else
                {
                    SetBalance(0);
                }
            }
            else
            {
                SetBalance(0);
            }
            SetAccountNumber(GenerateAccountNumber("S"));
        }

        // <summary>
        // Adds funds to the account
        // </summary>
        // <param name="amount">The amount to add to the account</param>
        // <returns>True if the amount is > 0, false if not</returns>
        public override bool PayInFunds(decimal amount)
        {
            
            if(base.PayInFunds(amount))
            {
                SetBalance(GetBalance() + (GetBalance() * 0.01m));

                return true;
            }
            else
            {
                return false;
            }
        }
        private string GenerateAccountNumber(string accountType)
        {
            return (++_lastAccountNumber).ToString("D4") + accountType;

        }
    }

    // <summary>
    // Child class of Account called CheckingAccount with a minimum balance of 10 and a service fee of 5
    // </summary>
    internal class CheckingAccount : Account
    {
        private const decimal MinimumBalance = 100;
        private static int _lastAccountNumber = 4000;
        private int _serviceFee = 5;

        // <summary>
        // Constructor for the CheckingAccount class setting the initial balance, account state, and account number
        // </summary>
        // <param name="amountInput">The initial balance of the account</param>
        // <returns>True if the amount is valid, false if not</returns>
        public CheckingAccount(string amountInput)
        {
            if (decimal.TryParse(amountInput, out decimal initialBalance))
            {
                if (initialBalance >= MinimumBalance)
                {
                    SetBalance(initialBalance);
                }
                else
                {
                    SetBalance(0);
                }
            }
            else
            {
                SetBalance(0);
            }
            if (initialBalance >= MinimumBalance)
            {
                SetBalance(initialBalance);
            }
            else
            {
                SetBalance(0);
            }
            SetAccountNumber(GenerateAccountNumber("C"));
        }

        // <summary>
        // Withdraws funds from the account
        // </summary>
        // <param name="amount">The amount to withdraw from the account</param>
        // <returns>True if the amount is > 0 and the balance is > 0, false if not</returns>
        public override bool WithdrawFunds(decimal amount)
        {
            if (amount > 0 )
            {   
                if (GetBalance() - amount - _serviceFee < 0)
                {
                    return false;
                }

                if (base.WithdrawFunds(amount))
                {
                    if(GetBalance() < MinimumBalance)
                    {
                        SetBalance(GetBalance() - _serviceFee);
                    }
                    return true;    
                }
                else 
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // <summary>
        // Generates the account number with unique letter at the end
        // </summary>
        // <param name="accountType">The type of account</param>
        // <returns>The account number</returns>
        private string GenerateAccountNumber(string accountType)
        {
            return (++_lastAccountNumber).ToString("D4") + accountType;

        }
    }

    // <summary>
    // Child class of Account called CDAccount with a minimum balance of 500 and a service fee of 8
    // </summary>
// <summary>
    // Child class of Account called CDAccount with a minimum balance of 500 and a service fee of 8
    // </summary>
    internal class CDAccount : Account
    {
        private const decimal MinimumBalance = 500;
        private static int _lastAccountNumber = 7000;
        private int _serviceFee = 8;

        // <summary>
        // Constructor for the CDAccount class setting the initial balance, account state, and account number
        // </summary>
        // <param name="amountInput">The initial balance of the account</param>
        // <returns>True if the amount is valid, false if not</returns>
        public CDAccount(string amountInput)
        {
            if (decimal.TryParse(amountInput, out decimal initialBalance))
            {
                if (initialBalance >= MinimumBalance)
                {
                    SetBalance(initialBalance);
                }
                else
                {
                    SetBalance(0);
                }
            }
            else
            {
                SetBalance(0);
            }
            if (initialBalance >= MinimumBalance)
            {
                SetBalance(initialBalance);
            }
            else
            {
                SetBalance(0);
            }
            SetAccountNumber(GenerateAccountNumber("D"));
        }

        // <summary>
        // Overridden WithdrawFunds method for CDAccount you cannot withdraw funds from a CD account
        // </summary>
        // <param name="amount">The amount to withdraw from the account</param>
        // <returns>False</returns>
        public override bool PayInFunds(decimal amount)
        {
            return false;
        }

        public override bool WithdrawFunds(decimal amount)
        {
            // Withdraw the funds with the service fee
            if (amount > 0)
            {
                if (GetBalance() - amount - _serviceFee < 0)
                {
                    return false;
                }

                if (base.WithdrawFunds(amount))
                {
                    if(GetBalance() < MinimumBalance)
                    {
                        SetBalance(GetBalance() - _serviceFee);
                    }
                    return true;    
                }
                else 
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // <summary>
        // Generates the account number with unique letter at the end
        // </summary>
        // <param name="accountType">The type of account</param>
        // <returns>The account number</returns>
        private string GenerateAccountNumber(string accountType)
        {
            return (++_lastAccountNumber).ToString("D4") + accountType;

        }
    }
}