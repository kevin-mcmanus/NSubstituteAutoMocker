namespace NSubstituteAutoMocker.UnitTests.SamplesToTest
{
    public class ClassWithInternalConstructor
    {
        internal IInternalInterface InternalInterface;

        public ClassWithInternalConstructor()
        {

        }

        internal ClassWithInternalConstructor(IInternalInterface internalInterface)
        {
            InternalInterface = internalInterface;
        }
    }
}
