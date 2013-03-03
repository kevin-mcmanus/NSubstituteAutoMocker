namespace NSubstituteAutoMocker.UnitTests.SamplesToTest
{
    public class ClassWithJustDefaultConstructor
    {
        public ClassWithJustDefaultConstructor()
        {         
        }

        public IDependency1 Dependency1 { get; set; }

        public IDependency2 Dependency2 { get; set; }
    }
}
