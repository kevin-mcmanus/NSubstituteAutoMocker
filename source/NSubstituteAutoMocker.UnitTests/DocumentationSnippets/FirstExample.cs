using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstituteAutoMocker.Standard;

namespace NSubstituteAutoMocker.UnitTests.DocumentationSnippets
{
    public class SavingsAccount
    {
        private readonly IInterestCalculator _interestCalculator;

        public SavingsAccount(IInterestCalculator interestCalculator)
        {
            _interestCalculator = interestCalculator;
        }

        public decimal Balance { get; private set; }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void WithDraw(decimal amount)
        {
            Balance -= amount;
        }

        public void ApplyInterest()
        {
            Balance += _interestCalculator.Calculate();
        }
    }

    public interface IInterestCalculator
    {
        decimal Calculate();
    }

    [TestClass]
    public class SavingsAccountTests
    {
        [TestMethod]
        public void ApplyInterestUpdatesTheBalance()
        {
            // Arange
            IInterestCalculator interestCalculator = Substitute.For<IInterestCalculator>();
            interestCalculator.Calculate().Returns(123);
            SavingsAccount savingsAccount = new SavingsAccount(interestCalculator);

            // Act
            savingsAccount.ApplyInterest();

            // Assert
            Assert.AreEqual(123, savingsAccount.Balance);
        }
    }

    [TestClass]
    public class SavingsAccountTestsWithNSubstituteAutoMocker
    {
        [TestMethod]
        public void ApplyInterestUpdatesTheBalance()
        {
            // Arange
            var automocker = new NSubstituteAutoMocker<SavingsAccount>();
            automocker.Get<IInterestCalculator>().Calculate().Returns(123);

            // Act
            automocker.ClassUnderTest.ApplyInterest();

            // Assert
            Assert.AreEqual(123, automocker.ClassUnderTest.Balance);
        }
    }
}
