namespace NSubstituteAutoMocker.UnitTests.SamplesToTest
{
    public class ClassWithPrivateDefaultConstructor
    {
        private ClassWithPrivateDefaultConstructor()
        {
        }

        public IDependency1 Dependency1 { get; set; }

        public IDependency2 Dependency2 { get; set; }
    }
}
