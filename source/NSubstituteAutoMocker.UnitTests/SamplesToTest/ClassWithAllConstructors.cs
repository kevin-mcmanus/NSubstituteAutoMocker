namespace NSubstituteAutoMocker.UnitTests.SamplesToTest
{
    public class ClassWithAllConstructors
    {
        public ClassWithAllConstructors()
        {
        }

        public ClassWithAllConstructors(IDependency1 dependency1, IDependency2 dependency2)
        {
            Dependency1 = dependency1;
            Dependency2 = dependency2;
        }

        public IDependency1 Dependency1 { get; set; }

        public IDependency2 Dependency2 { get; set; }
    }
}
