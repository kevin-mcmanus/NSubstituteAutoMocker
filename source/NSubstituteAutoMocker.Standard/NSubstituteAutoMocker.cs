using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSubstitute;

namespace NSubstituteAutoMocker.Standard
{
    public class NSubstituteAutoMocker<T> where T : class
    {
        private readonly Dictionary<ParameterInfo, object> _constructors;

        public NSubstituteAutoMocker()
            :this(null, null)
        {
        }

        public NSubstituteAutoMocker(Type[] parameterTypes)
            : this(parameterTypes, null)
        {
        }

        /* the code below will not currently work as expected - the override code will not even get called
        public NSubstituteAutoMocker(Func<ParameterInfo, object, object> parameterOverrideFunc)
            : this(null, parameterOverrideFunc)
        {
        }*/

        public NSubstituteAutoMocker(Type[] parameterTypes, Func<ParameterInfo, object, object> parameterOverrideFunc)
        {
            _constructors = new Dictionary<ParameterInfo, object>();

            var constructorInfo = MatchConstructorWithParameters(typeof (T), parameterTypes);
            
            var parameters = constructorInfo.GetParameters();
            foreach (ParameterInfo info in parameters)
            {
                Type type = info.ParameterType;
                var constructorArg = CreateInstance(type);
                if (parameterOverrideFunc != null)
                {
                    constructorArg = parameterOverrideFunc(info, constructorArg);
                }
                _constructors.Add(info, constructorArg);
            }

            object[] args = _constructors.Values.ToArray();
            ClassUnderTest = Activator.CreateInstance(typeof(T), args) as T;
        }

        private object CreateInstance(Type type)
        {
            if (type.IsPrimitive)
            {
                return Activator.CreateInstance(type);
            }

            if (type == typeof(string))
            {
                return null;
            }

            return Substitute.For(new Type[] { type }, null);
        }

        private ConstructorInfo MatchConstructorWithParameters(Type type, Type[] parameterTypes)
        {
            ConstructorInfo result;
            if (type.GetConstructors().Length == 0)
            {
                // this might occur when there are no public constructors
                result = null;
            }
            else if (parameterTypes == null)
            {
                result = GetHighestParameterCountConstructor(type);
            }
            else if (parameterTypes.Length == 0)
            {
                result = typeof (T).GetConstructor(Type.EmptyTypes);
            }
            else
            {
                result = typeof(T).GetConstructor((Type[]) parameterTypes);
            }
            
            if (result == null) throw new ConstructorMatchException();

            return result;
        }

        private static ConstructorInfo GetHighestParameterCountConstructor(Type type)
        {
            ConstructorInfo result = type.GetConstructors()[0];
            foreach (ConstructorInfo constructorInfo in type.GetConstructors())
            {
                if (constructorInfo.GetParameters().Length > result.GetParameters().Length)
                {
                    result = constructorInfo;
                }
            }
            return result;
        }

        public T ClassUnderTest { get; private set; }

        public TArg Get<TArg>()
        {
            return Get<TArg>(null);
        }

        public TArg Get<TArg>(string name)
        {
            var parameters = _constructors.Keys.Where(info => info.ParameterType == typeof(TArg));
            if (parameters.Count() == 0)
            {
                throw new ConstructorParameterNotFoundException();
            }
            else if (parameters.Count() > 1)
            {
                if (name == null || parameters.Count(info => info.Name == name) != 1)
                {
                    throw new ConstructorParameterNotFoundException();
                }
                parameters = parameters.Where(info => info.Name == name);
            }
            return (TArg)_constructors[parameters.Single()];
        }
    }
}
