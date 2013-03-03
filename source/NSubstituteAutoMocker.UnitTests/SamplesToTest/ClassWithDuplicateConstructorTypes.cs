namespace NSubstituteAutoMocker.UnitTests.SamplesToTest
{
    public class ClassWithDuplicateConstructorTypes
    {
        public ClassWithDuplicateConstructorTypes()
        {
        }

        public ClassWithDuplicateConstructorTypes(IDependency1 dependencyOne, IDependency1 dependencyTwo)
        {
            DependencyOne = dependencyOne;
            DependencyTwo = dependencyTwo;
        }

        public IDependency1 DependencyOne { get; set; }

        public IDependency1 DependencyTwo { get; set; }
    }
}
