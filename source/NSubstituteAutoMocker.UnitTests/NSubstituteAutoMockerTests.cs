using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstituteAutoMocker.Standard;
using NSubstituteAutoMocker.UnitTests.SamplesToTest;

namespace NSubstituteAutoMocker.UnitTests
{
    [TestClass]
    public class NSubstituteAutoMockerTests
    {
        [TestClass]
        public class DefaultConstructor
        {
            [TestMethod]
            public void UsesDefaultConstructorIfAvailable()
            {
                NSubstituteAutoMocker<ClassWithJustDefaultConstructor> autoMocker = 
                    new NSubstituteAutoMocker<ClassWithJustDefaultConstructor>();
                Assert.IsNotNull(autoMocker.ClassUnderTest);
            }

            /* Don't think it make sense to throw exceptpion in this case
            [TestMethod]
            [ExpectedException(typeof(ConstructorMatchException))]
            public void ThrowsExceptionIfNotAvailable()
            {
                NSubstituteAutoMocker<ClassWithNoDefaultConstructor> autoMocker = 
                    new NSubstituteAutoMocker<ClassWithNoDefaultConstructor>();
            }*/

            [TestMethod]
            [ExpectedException(typeof(ConstructorMatchException))]
            public void ThrowsExceptionIfPrivate()
            {
                NSubstituteAutoMocker<ClassWithPrivateDefaultConstructor> autoMocker = 
                    new NSubstituteAutoMocker<ClassWithPrivateDefaultConstructor>();
            }

            [TestMethod]
            public void CanOverrideParameterViaEvent()
            {
                Assert.Inconclusive();
            }

            [TestMethod]
            public void ClassUnderTestCanBeSealed()
            {
                NSubstituteAutoMocker<SealedClass> autoMocker =
                    new NSubstituteAutoMocker<SealedClass>();
                Assert.IsNotNull(autoMocker.ClassUnderTest);
            }

            [TestMethod]
            public void PrimativeParameterTypesGetSetToDefaultValue()
            {
                NSubstituteAutoMocker<ClassWithPrimativeConstructors> autoMocker =
                    new NSubstituteAutoMocker<ClassWithPrimativeConstructors>();
                Assert.AreEqual(0, autoMocker.ClassUnderTest.IntValue);
                Assert.AreEqual(null, autoMocker.ClassUnderTest.StringValue);
            }
        }

        [TestClass]
        public class ConstructorWithArgs
        {
            [TestMethod]
            public void UsesEquivilantConstructorIfAvailable()
            {
                NSubstituteAutoMocker<ClassWithAllConstructors> autoMocker = 
                    new NSubstituteAutoMocker<ClassWithAllConstructors>(new Type[] {typeof(IDependency1), typeof(IDependency2)});
                Assert.IsNotNull(autoMocker.ClassUnderTest);
                Assert.IsNotNull(autoMocker.ClassUnderTest.Dependency1);
                Assert.IsNotNull(autoMocker.ClassUnderTest.Dependency2);
            }

            [TestMethod]
            public void NullParameterUsesDefaultConstructor()
            {
                NSubstituteAutoMocker<ClassWithAllConstructors> autoMocker =
                    new NSubstituteAutoMocker<ClassWithAllConstructors>(null, null);
                Assert.IsNotNull(autoMocker.ClassUnderTest);
            }

            [TestMethod]
            public void EmptyParameterUsesDefaultConstructor()
            {
                NSubstituteAutoMocker<ClassWithAllConstructors> autoMocker = 
                    new NSubstituteAutoMocker<ClassWithAllConstructors>(new Type[] {});
                Assert.IsNotNull(autoMocker.ClassUnderTest); 
            }

            [TestMethod]
            [ExpectedException(typeof(ConstructorMatchException))]
            public void ThrowsExceptionIfNoMatchAvailable()
            {
                NSubstituteAutoMocker<ClassWithAllConstructors> autoMocker =
                    new NSubstituteAutoMocker<ClassWithAllConstructors>(new Type[] { typeof(IDependency2), typeof(IDependency1) });
            }

            [TestMethod]
            public void CanOverrideParameterViaEvent()
            {
                NSubstituteAutoMocker<ClassWithAllConstructors> autoMocker =
                    new NSubstituteAutoMocker<ClassWithAllConstructors>(new Type[] {typeof(IDependency1), typeof(IDependency2)},
                        (paramInfo, obj) =>
                        {
                            if (paramInfo.ParameterType == typeof(IDependency1))
                                return new Dependency1VitoImplementation();
                            else
                                return obj;
                        });
                Assert.IsInstanceOfType(autoMocker.ClassUnderTest.Dependency1, typeof(Dependency1VitoImplementation));
            }
        }

        [TestClass]
        public class Get
        {
            [TestMethod]
            public void AvailableTypeReturned()
            {
                NSubstituteAutoMocker<ClassWithAllConstructors> autoMocker =
                    new NSubstituteAutoMocker<ClassWithAllConstructors>();
                Assert.IsNotNull(autoMocker.Get<IDependency1>());
                Assert.IsNotNull(autoMocker.Get<IDependency2>());
            }

            [TestMethod]
            [ExpectedException(typeof(ConstructorParameterNotFoundException))]
            public void UnavailableTypeThrowsException()
            {
                NSubstituteAutoMocker<ClassWithAllConstructors> autoMocker =
                    new NSubstituteAutoMocker<ClassWithAllConstructors>();
                autoMocker.Get<IOverdraft>();
            }

            [TestMethod]
            [ExpectedException(typeof(ConstructorParameterNotFoundException))]
            public void MutipleAvailableTypesThrowsExceptionIfNameNotSpecified()
            {
                NSubstituteAutoMocker<ClassWithDuplicateConstructorTypes> autoMocker =
                    new NSubstituteAutoMocker<ClassWithDuplicateConstructorTypes>();
                autoMocker.Get<IDependency1>();
            }

            [TestMethod]
            public void MutipleAvailableTypesWithNameReturned()
            {
                NSubstituteAutoMocker<ClassWithDuplicateConstructorTypes> autoMocker =
                    new NSubstituteAutoMocker<ClassWithDuplicateConstructorTypes>();
                Assert.IsNotNull(autoMocker.Get<IDependency1>("dependencyTwo"));
            }
        }

        // TODO Do I need to test the use of generics or collections?
        // TODO Do I need to test the visibility of classes/interfaces?
    }
}
