namespace NSubstituteAutoMocker.UnitTests.SamplesToTest
{
    public sealed class SealedClass
    {
        public SealedClass()
        {
        }

        public SealedClass(IDependency1 dependency1, IDependency2 dependency2)
        {
            Dependency1 = dependency1;
            Dependency2 = dependency2;
        }

        public IDependency1 Dependency1 { get; set; }

        public IDependency2 Dependency2 { get; set; }
    }
}
