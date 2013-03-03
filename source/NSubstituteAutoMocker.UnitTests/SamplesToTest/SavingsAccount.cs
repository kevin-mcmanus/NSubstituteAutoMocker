namespace NSubstituteAutoMocker.UnitTests.SamplesToTest
{
    class SavingsAccount
    {
        private readonly IInterestCalculator _interestCalculator;
        private readonly IOverdraft _overdraft;

        public SavingsAccount(IInterestCalculator interestCalculator)
            : this(interestCalculator, null)
        {
        }

        public SavingsAccount(IOverdraft overdraft)
            : this(null, overdraft)
        {
        }

        public SavingsAccount(IInterestCalculator interestCalculator, IOverdraft overdraft)
        {
            _interestCalculator = interestCalculator;
            _overdraft = overdraft;
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
}
