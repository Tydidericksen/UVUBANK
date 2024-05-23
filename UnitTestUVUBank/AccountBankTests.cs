namespace UnitTestUVUBank;
using Xunit;
using BankingApp;

public class AccountBankTests
{
    // <summary>
    // Tests the IsNumberOfAccountsValid function
    // </summary>
    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    public void IsNumberOfAccountsValid_ValidNumber_ReturnsTrue(string input)
    {
        // Act
        bool result = AccountBank.IsNumberOfAccountsValid(input);

        // Assert
        Assert.True(result);
    }

    // <summary>
    // Tests the IsNumberOfAccountsValid function for invalid numbers
    // </summary>
    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("a")]
    public void IsNumberOfAccountsValid_InvalidNumber_ReturnsFalse(string input)
    {
        // Act
        bool result = AccountBank.IsNumberOfAccountsValid(input);

        // Assert
        Assert.False(result);
    }

    // <summary>
    // Tests the StoreAccount function for a new account
    // </summary>
    [Fact]
    public void StoreAccount_NewAccount_ReturnsTrue()
    {
        // Arrange
        AccountBank bank = new AccountBank();
        IAccount account = new SavingsAccount("100");

        // Act
        bool result = bank.StoreAccount(account, "123");

        // Assert
        Assert.True(result);
    }

    // <summary>
    // Tests the StoreAccount function for an existing account returning false
    // </summary>
    [Fact]
    public void StoreAccount_ExistingAccount_ReturnsFalse()
    {
        // Arrange
        AccountBank bank = new AccountBank();
        IAccount account = new SavingsAccount("100");
        bank.StoreAccount(account, "123");

        // Act
        bool result = bank.StoreAccount(account, "123");

        // Assert
        Assert.False(result);
    }

    // <summary>
    // Tests the FindAccount function for an existing account
    // </summary>
    [Fact]
    public void FindAccount_ExistingAccount_ReturnsAccount()
    {
        // Arrange
        AccountBank bank = new AccountBank();
        IAccount account = new SavingsAccount("100");
        bank.StoreAccount(account, "123");

        // Act
        IAccount result = bank.FindAccount("123");

        // Assert
        Assert.Equal(account, result);
    }

    // <summary>
    // Tests the FindAccount function for a non-existing account
    // </summary>
    [Fact]
    public void FindAccount_NonExistingAccount_ReturnsNull()
    {
        // Arrange
        AccountBank bank = new AccountBank();

        // Act
        IAccount result = bank.FindAccount("123");

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void GetAllAccounts_NoAccounts_ReturnsEmptyDictionary()
    {
        // Arrange
        AccountBank bank = new AccountBank();

        // Act
        var result = bank.GetAllAccounts();

        // Assert
        Assert.Empty(result);
    }

    // <summary>
    // Tests the GetAllAccounts function for a bank with accounts
    // </summary>
    [Fact]
    public void GetAllAccounts_WithAccounts_ReturnsDictionary()
    {
        // Arrange
        AccountBank bank = new AccountBank();
        IAccount account1 = new SavingsAccount("100");
        IAccount account2 = new SavingsAccount("200");
        bank.StoreAccount(account1, "123");
        bank.StoreAccount(account2, "456");

        // Act
        var result = bank.GetAllAccounts();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(account1, result["123"]);
        Assert.Equal(account2, result["456"]);
    }

}