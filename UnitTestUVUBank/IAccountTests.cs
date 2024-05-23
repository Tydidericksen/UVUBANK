namespace UnitTestUVUBank;
using Xunit;
using BankingApp;

public class IAccountTests
{
    // <summary>
    // Tests the GetBalance function for a valid balance
    // </summary>
    [Theory]
    [InlineData("100")]
    [InlineData("200")]
    [InlineData("300")]
    public void IAccount_GetBalance_ValidAmount_ReturnsTrue(string balance)
    {
        // Arrange
        IAccount account = new SavingsAccount(balance);

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
    public void IAccount_PayInFunds_InvalidAmount_ReturnsFalse(decimal payInAmount)
    {
        // Arrange
        IAccount account = new SavingsAccount("100");

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
    public void IAccount_WithdrawFunds_ValidAmount_ReturnsTrue(decimal withdrawAmount)
    {
        // Arrange
        IAccount account = new SavingsAccount("200");

        // Act
        bool result = account.WithdrawFunds(withdrawAmount);

        // Assert
        Assert.True(result);
        Assert.Equal(200 - withdrawAmount, account.GetBalance());
    }

    // <summary>
    // Tests the PayInFunds function for a valid amount
    // </summary>
    [Theory]
    [InlineData(-50)]
    [InlineData(0)]
    [InlineData(300)]
    public void IAccount_WithdrawFunds_InvalidAmount_ReturnsFalse(decimal withdrawAmount)
    {
        // Arrange
        IAccount account = new SavingsAccount("200");

        // Act
        bool result = account.WithdrawFunds(withdrawAmount);

        // Assert
        Assert.False(result);
        Assert.Equal(200, account.GetBalance());
    }

    // <summary>
    // Tests the SetBalance function for a valid balance
    // </summary>
    [Theory]
    [InlineData("John")]
    public void IAccount_SetName_ValidName_ReturnsTrue(string name)
    {
        // Arrange
        IAccount account = new SavingsAccount("200");

        // Act
        bool result = account.SetName(name);

        // Assert
        Assert.True(result);
        Assert.Equal(name, account.GetName());
    }

    // <summary>
    // Tests the SetBalance function for a valid balance
    // </summary>
    [Theory]
    [InlineData("")]
    public void IAccount_SetName_InvalidName_ReturnsFalse(string name)
    {
        // Arrange
        IAccount account = new SavingsAccount("200");

        // Act
        bool result = account.SetName(name);

        // Assert
        Assert.False(result);
    }

    // <summary>
    // Tests the SetBalance function for a valid balance
    // </summary>
    [Theory]
    [InlineData("1234 Main St")]
    public void IAccount_SetAddress_ValidAddress_ReturnsTrue(string address)
    {
        // Arrange
        IAccount account = new SavingsAccount("200");

        // Act
        bool result = account.SetAddress(address);

        // Assert
        Assert.True(result);
        Assert.Equal(address, account.GetAddress());
    }

    // <summary>
    // Tests the SetBalance function for a valid balance
    // </summary>
    [Theory]
    [InlineData("")]
    public void IAccount_SetAddress_InvalidAddress_ReturnsFalse(string address)
    {
        // Arrange
        IAccount account = new SavingsAccount("200");

        // Act
        bool result = account.SetAddress(address);

        // Assert
        Assert.False(result);
    }

    // <summary>
    // Tests the SetState function for a valid state
    // </summary>
    [Theory]
    [InlineData(AccountState.Active)]
    public void IAccount_SetState_ValidState_ReturnsTrue(AccountState state)
    {
        // Arrange
        IAccount account = new SavingsAccount("200");

        // Act
        account.SetState(state);

        // Assert
        Assert.Equal(state, account.GetState());
    }


    // <summary>
    // Tests the SetState function for a valid state
    // </summary>
    [Theory]
    [InlineData("50")]
    [InlineData("100")]
    [InlineData("200")]
    [InlineData("2.50")]
    public void IAccount_IsAmountValid_ValidAmount_ReturnsTrue(string amountInput)
    {
        // Arrange
        IAccount account = new SavingsAccount("200");

        // Act
        bool result = account.IsAmountValid(amountInput);

        // Assert
        Assert.True(result);
    }

    // <summary>
    // Tests the SetState function for a valid state
    // </summary>
    [Theory]
    [InlineData("-50")]
    [InlineData("0")]
    [InlineData("2.50.50")]
    public void IAccount_IsAmountValid_InvalidAmount_ReturnsFalse(string amountInput)
    {
        // Arrange
        IAccount account = new SavingsAccount("200");

        // Act
        bool result = account.IsAmountValid(amountInput);

        // Assert
        Assert.False(result);
    }

}
