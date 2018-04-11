using System;

namespace NSubstituteAutoMocker.Standard
{
    public class ConstructorMatchException : Exception
    {
        public ConstructorMatchException()
            : base(String.Format("Constructor with supplied types cannot be found.{0}This might be a result of a missing (or private) default constructor, or a miss match in the paramater list given.", Environment.NewLine))
        {
        }
    }
}
