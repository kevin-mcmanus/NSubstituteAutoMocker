using System;

namespace NSubstituteAutoMocker
{
    public class ConstructorParameterNotFoundException : Exception
    {
        public ConstructorParameterNotFoundException()
            : base("The requested parameter type was not used in the original call to the constructor")
        {
        }
    }
}
