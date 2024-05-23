namespace UnitTestUVUBank;
using Xunit;
using BankingApp;

public class AccountTests
{
    // <summary>
    // Tests the GetBalance function for a valid balance
    // </summary>
    [Theory]
    [InlineData("100")]
    [InlineData("200")]
    [InlineData("300")]
    public void SavingsAccount_GetBalance_ValidAmount_ReturnsTrue(string balance)
    {
        // Arrange
        Account account = new SavingsAccount(balance);

        // Act
        decimal result = account.GetBalance();

        // Assert
        Assert.Equal(decimal.Parse(balance), result);
    }

    // <summary>
    // Tests the PayInFunds function for a valid amount
    // </summary>
    [Theory]
    [InlineData(-50)]
    [InlineData(0)]
    public void SavingsAccount_PayInFunds_InvalidAmount_ReturnsFalse(decimal payInAmount)
    {
        // Arrange
        SavingsAccount account = new SavingsAccount("100");

        // Act
        bool result = account.PayInFunds(payInAmount);

        // Assert
        Assert.False(result);
        Assert.Equal(100, account.GetBalance());
    }

    // <summary>
    // Tests the PayInFunds function for a valid amount
    // </summary>
    [Theory]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(2.50)]
    public void SavingsAccount_WithdrawFunds_ValidAmount_ReturnsTrue(decimal withdrawAmount)
    {
        // Arrange
        SavingsAccount account = new SavingsAccount("200");

        // Act
        bool result = account.WithdrawFunds(withdrawAmount);

        // Assert
        Assert.True(result);
        Assert.Equal(200 - withdrawAmount, account.GetBalance());
    }

    // <summary>
    // Tests the PayInFunds function for an invalid amount
    // </summary>
    [Theory]
    [InlineData(-50)]
    [InlineData(0)]
    [InlineData(300)]
    public void SavingsAccount_WithdrawFunds_InvalidAmount_ReturnsFalse(decimal withdrawAmount)
    {
        // Arrange
        SavingsAccount account = new SavingsAccount("200");

        // Act
        bool result = account.WithdrawFunds(withdrawAmount);

        // Assert
        Assert.False(result);
        Assert.Equal(200, account.GetBalance());
    }

    // <summary>
    // Tests the GetBalance function for a valid balance
    // </summary>
    [Theory]
    [InlineData("100")]
    [InlineData("200")]
    [InlineData("300")]
    public void CheckingAccount_GetBalance_ValidAmount_ReturnsTrue(string balance)
    {
        // Arrange
        Account account = new CheckingAccount(balance);

        // Act
        decimal result = account.GetBalance();

        // Assert
        Assert.Equal(decimal.Parse(balance), result);
    }

    // <summary>
    // Tests the PayInFunds function for a valid amount
    // </summary>
    [Theory]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(200)]
    public void CheckingAccount_PayInFunds_ValidAmount_ReturnsTrue(decimal payInAmount)
    {
        // Arrange
        CheckingAccount account = new CheckingAccount("100");

        // Act
        bool result = account.PayInFunds(payInAmount);

        // Assert
        Assert.True(result);
        Assert.Equal(100 + payInAmount, account.GetBalance());
    }

    // <summary>
    // Tests the PayInFunds function for an invalid amount
    // </summary>
    [Theory]
    [InlineData(-50)]
    [InlineData(0)]
    public void CheckingAccount_PayInFunds_InvalidAmount_ReturnsFalse(decimal payInAmount)
    {
        // Arrange
        CheckingAccount account = new CheckingAccount("100");

        // Act
        bool result = account.PayInFunds(payInAmount);

        // Assert
        Assert.False(result);
        Assert.Equal(100, account.GetBalance());
    }

    // <summary>
    // Tests the WithdrawFunds function for a valid amount
    // </summary>
    [Theory]
    [InlineData(50)]
    [InlineData(100)]
    public void CheckingAccount_WithdrawFunds_ValidAmount_ReturnsTrue(decimal withdrawAmount)
    {
        // Arrange
        CheckingAccount account = new CheckingAccount("200");

        // Act
        bool result = account.WithdrawFunds(withdrawAmount);

        // Assert
        Assert.True(result);
        Assert.Equal(200 - withdrawAmount, account.GetBalance());
    }

    // <summary>
    // Tests the WithdrawFunds function for an invalid amount
    // </summary>
    [Theory]
    [InlineData(-50)]
    [InlineData(0)]
    [InlineData(200)]
    [InlineData(300)]
    public void CheckingAccount_WithdrawFunds_InvalidAmount_ReturnsFalse(decimal withdrawAmount)
    {
        // Arrange
        CheckingAccount account = new CheckingAccount("200");

        // Act
        bool result = account.WithdrawFunds(withdrawAmount);

        // Assert
        Assert.False(result);
        Assert.Equal(200, account.GetBalance());
    }

    // <summary>
    // Tests the GetBalance function for a valid balance
    // </summary>
    [Theory]
    [InlineData("500")]
    [InlineData("1000")]

    public void CDAccount_GetBalance_ValidAmount_ReturnsTrue(string balance)
    {
        // Arrange
        CDAccount account = new CDAccount(balance);

        // Act
        decimal result = account.GetBalance();

        // Assert
        Assert.Equal(decimal.Parse(balance), result);
    }
    
    // <summary>
    // Tests the PayInFunds function for a valid amount
    // </summary>
    [Theory]
    [InlineData(-50)]
    [InlineData(0)]
    public void CDAccount_PayInFunds_InvalidAmount_ReturnsFalse(decimal payInAmount)
    {
        // Arrange
        CDAccount account = new CDAccount("500");

        // Act
        bool result = account.PayInFunds(payInAmount);

        // Assert
        Assert.False(result);
        Assert.Equal(500, account.GetBalance());
    }

    // <summary>
    // Tests the PayInFunds function for a valid amount
    // </summary>
    [Theory]
    [InlineData("Ty")]
    [InlineData("Alex Smith")]
    public void Account_GetName_SetName_ValidName_ReturnsTrue(string name)
    {
        // Arrange
        SavingsAccount account = new SavingsAccount("100");

        // Act
        account.SetName(name);

        // Assert
        Assert.Equal(name, account.GetName());
    }

    // <summary>
    // Tests the GetAddress function for a valid address
    // </summary>
    [Theory]
    [InlineData("123456789")]
    [InlineData("987654321")]

    public void Account_GetAccountNumber_SetAccountNumber_ValidAccountNumber_ReturnsTrue(string accountNumber)
    {
        // Arrange
        SavingsAccount account = new SavingsAccount("100");

        // Act
        account.SetAccountNumber(accountNumber);

        // Assert
        Assert.Equal(accountNumber, account.GetAccountNumber());
    }

    // <summary>
    // Tests the IsAmountValid function for a valid amount
    // </summary>
    [Theory]
    [InlineData("5.50")]
    [InlineData("10")]
    [InlineData("100")]   
    public void Account_IsAmountValid_ValidAmount_ReturnsTrue(string amount)
    {
        // Arrange
        Account account = new SavingsAccount("100");

        // Act
        bool result = account.IsAmountValid(amount);

        // Assert
        Assert.True(result);
    }

}
