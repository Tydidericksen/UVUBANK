using System;

// interface IAccount
namespace BankingApp
{
    public interface IAccount
    {
        // Function definitions for SetName, GetName, SetAddress, GetAddress, PayInFunds, WithdrawFunds, SetBalance, GetBalance, SetState
        bool SetName(string inName);
        string GetName();
        bool SetAddress(string inAddress);
        string GetAddress();
        bool PayInFunds(decimal amount);
        bool WithdrawFunds(decimal amount);
        void SetBalance(decimal amount);
        decimal GetBalance();
        void SetState(AccountState state);
        AccountState GetState();
        string GetAccountNumber();
        bool IsAmountValid(string amountInput);

    }
}