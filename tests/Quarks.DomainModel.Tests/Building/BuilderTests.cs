using System;
using NUnit.Framework;
using Quarks.DomainModel.Building;

namespace Quarks.DomainModel.Tests.Building
{
    [TestFixture]
    public class BuilderTests
    {
        [Test]
        public void Can_Populate_Field()
        {
            var instance = new Builder<FakeClass>().With(t => t.Field, 10).Create();

            Assert.That(instance.Field, Is.EqualTo(10));
        }

        [Test]
        public void Can_Populate_Property()
        {
            var instance = new Builder<FakeClass>().With(t => t.Property, 10).Create();

            Assert.That(instance.Property, Is.EqualTo(10));
        }

        [Test]
        public void Can_Populate_ReadOnly_Property()
        {
            var instance = new Builder<FakeClass>().With(t => t.ReadOnlyProperty, 10).Create();

            Assert.That(instance.ReadOnlyProperty, Is.EqualTo(10));
        }

        [Test]
        public void Can_Be_Constructed_With_Instance()
        {
            var instance = new FakeClass {Field = 10};

            var created = new Builder<FakeClass>(instance).Create();

            Assert.That(created, Is.SameAs(instance));
        }

        [Test]
        public void Can_Be_Constructed_With_Null_Instance()
        {
            var created = new Builder<FakeClass>(null).Create();

            Assert.That(created, Is.Null);
        }

        [Test]
        public void CanNot_Populate_Method()
        {
            var builder = new Builder<FakeClass>();

            Assert.Throws<InvalidOperationException>(
                () => builder.With(t => t.Method(), 10));
        }

        [Test]
        public void Creating_Class_With_No_ParameterLess_Constructor_Throws_An_Exception()
        {
            Assert.Throws<MissingMethodException>(
               () => new Builder<ClassWithNoParameterlessConstructor>().Create());
        }

        private class FakeClass
        {
            public int Field;

            public int Property { get; set; }

            public int ReadOnlyProperty { get; private set; }

            public int Method()
            {
                return 0;
            }
        }

        private class ClassWithNoParameterlessConstructor
        {
            public ClassWithNoParameterlessConstructor(int argument)
            {
            }
        }
    }
}