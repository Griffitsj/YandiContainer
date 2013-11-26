using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YandiContainer;
using YandiContainer.Lifetime;
using YandiContainer.Registration;

namespace YandiContainerTest
{
    [TestClass]
    public class ContainerTest
    {
        Container container;

        [TestInitialize]
        public void TestInitialize()
        {
            this.container = new Container();

            this.container.AddRegistrationEntry(typeof(TestClassA), new RegistrationEntry(typeof(TestClassA), new ContainerLifetime()));
            this.container.AddRegistrationEntry(typeof(TestClassB), new RegistrationEntry(typeof(TestClassB)));
            this.container.AddRegistrationEntry(typeof(TestClassC), new RegistrationEntry(typeof(TestClassC)));
            this.container.AddRegistrationEntry(typeof(TestClassD), new RegistrationEntry(typeof(TestClassD), new PerResolveLifetime()));
            this.container.AddRegistrationEntry(typeof(ITestClassC), new RegistrationEntry(typeof(TestClassC)));
        }

        [TestMethod]
        public void Wibble()
        {
            var a = (TestClassA)this.container.Resolve(typeof(TestClassA));
            Assert.IsNotNull(a);
            Assert.IsNotNull(a.Container);
            var b = (TestClassB)this.container.Resolve(typeof(TestClassB));
            Assert.IsNotNull(b);
            Assert.IsNotNull(b.TestClassA);
            Assert.AreSame(a, b.TestClassA);

            var c = (TestClassC)this.container.Resolve(typeof(TestClassC));
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.TestClassA);
            Assert.AreSame(a, c.TestClassA);
            Assert.IsNotNull(c.TestClassB);
            Assert.AreNotSame(b, c.TestClassB);

            var c2 = (TestClassC)this.container.Resolve(typeof(TestClassC));
            Assert.IsNotNull(c2);
            Assert.AreNotSame(c, c2);
        }

        [TestMethod]
        public void AutoRegistration()
        {
            var autoC = (TestClassAutoC)this.container.Resolve(typeof(TestClassAutoC));
            Assert.IsNotNull(autoC);
            Assert.IsNotNull(autoC.TestClassA);
            Assert.IsNotNull(autoC.TestClassB);
        }

        [TestMethod]
        public void PerResolveRegistration()
        {
            var c = (TestClassC)this.container.Resolve(typeof(TestClassC));
            Assert.IsNotNull(c);
            Assert.AreSame(c.TestClassD, c.TestClassB.TestClassD);

            var c2 = (TestClassC)this.container.Resolve(typeof(TestClassC));
            Assert.AreNotSame(c.TestClassD, c2.TestClassD);
        }

        [TestMethod]
        public void InterfaceMapping()
        {
            var c = this.container.Resolve(typeof(ITestClassC));
            Assert.IsNotNull(c);
            Assert.IsInstanceOfType(c, typeof(TestClassC));
        }

        [TestMethod]
        public void HierarchicalLifetime()
        {
            Assert.Inconclusive();
        }

        public class TestClassA
        {
            public TestClassA(Container container)
            {
                this.Container = container;
            }

            public Container Container { get; private set; }
        }

        public class TestClassB
        {
            public TestClassB(TestClassA testClassA, TestClassD testClassD)
            {
                this.TestClassA = testClassA;
                this.TestClassD = testClassD;
            }

            public TestClassA TestClassA { get; private set; }
            public TestClassD TestClassD { get; private set; }
        }
        
        public interface ITestClassC
        {
        }

        public class TestClassC : ITestClassC
        {
            public TestClassC(TestClassA testClassA, TestClassB testClassB, TestClassD testClassD)
            {
                this.TestClassA = testClassA;
                this.TestClassB = testClassB;
                this.TestClassD = testClassD;
            }

            public TestClassA TestClassA { get; private set; }
            public TestClassB TestClassB { get; private set; }
            public TestClassD TestClassD { get; private set; }
        }

        public class TestClassAutoC
        {
            public TestClassAutoC(TestClassA testClassA, TestClassB testClassB)
            {
                this.TestClassA = testClassA;
                this.TestClassB = testClassB;
            }

            public TestClassA TestClassA { get; private set; }
            public TestClassB TestClassB { get; private set; }
        }

        public class TestClassD
        {            
        }
    }
}