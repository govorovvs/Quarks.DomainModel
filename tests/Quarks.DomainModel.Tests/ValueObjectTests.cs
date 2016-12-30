using JetBrains.Annotations;
using NUnit.Framework;

namespace Quarks.DomainModel.Tests
{
    [TestFixture]
    public class ValueObjectTests
    {
        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class Address : ValueObject<Address>
        {
            public Address(string address1, string city, string state)
            {
                Address1 = address1;
                City = city;
                State = state;
            }

            public string Address1 { get; }

            public string City { get; }

            public string State { get; }
        }

        private class ExpandedAddress : Address
        {
            public ExpandedAddress(string address1, string address2, string city, string state)
                : base(address1, city, state)
            {
                Address2 = address2;
            }

            public string Address2 { get; }
        }

        [Test]
        public void AddressEqualsWorksWithIdenticalAddresses()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address1", "Austin", "TX");

            Assert.That(address.Equals(address2), Is.True);
        }

        [Test]
        public void AddressEqualsWorksWithNonIdenticalAddresses()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address2", "Austin", "TX");

            Assert.That(address.Equals(address2), Is.False);
        }

        [Test]
        public void AddressEqualsWorksWithNulls()
        {
            Address address = new Address(null, "Austin", "TX");
            Address address2 = new Address("Address2", "Austin", "TX");

            Assert.That(address.Equals(address2), Is.False);
        }

        [Test]
        public void AddressEqualsWorksWithNullsOnOtherObject()
        {
            Address address = new Address("Address2", "Austin", "TX");
            Address address2 = new Address("Address2", null, "TX");

            Assert.That(address.Equals(address2), Is.False);
        }

        [Test]
        public void AddressEqualsIsReflexive()
        {
            Address address = new Address("Address1", "Austin", "TX");

            Assert.That(address.Equals(address), Is.True);
        }

        [Test]
        public void AddressEqualsIsSymmetric()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address2", "Austin", "TX");

            Assert.That(address.Equals(address2), Is.False);
            Assert.That(address2.Equals(address), Is.False);
        }

        [Test]
        public void AddressEqualsIsTransitive()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address1", "Austin", "TX");
            Address address3 = new Address("Address1", "Austin", "TX");

            Assert.That(address.Equals(address2), Is.True);
            Assert.That(address2.Equals(address3), Is.True);
            Assert.That(address.Equals(address3), Is.True);
        }

        [Test]
        public void AddressOperatorsWork()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address1", "Austin", "TX");
            Address address3 = new Address("Address2", "Austin", "TX");

            Assert.That(address == address2, Is.True);
            Assert.That(address2 != address3, Is.True);
        }

        [Test]
        public void DerivedTypesBehaveCorrectly()
        {
            Address address = new Address("Address1", "Austin", "TX");
            ExpandedAddress address2 = new ExpandedAddress("Address1", "Apt 123", "Austin", "TX");

            Assert.That(address.Equals(address2), Is.False);
            Assert.That(address == address2, Is.False);
        }

        [Test]
        public void EqualValueObjectsHaveSameHashCode()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address1", "Austin", "TX");

            Assert.That(address.GetHashCode() == address2.GetHashCode(), Is.True);
        }

        [Test]
        public void TransposedValuesGiveDifferentHashCodes()
        {
            Address address = new Address(null, "Austin", "TX");
            Address address2 = new Address("TX", "Austin", null);

            Assert.That(address.GetHashCode() == address2.GetHashCode(), Is.False);
        }

        [Test]
        public void UnequalValueObjectsHaveDifferentHashCodes()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address2", "Austin", "TX");

            Assert.That(address.GetHashCode() == address2.GetHashCode(), Is.False);
        }

        [Test]
        public void TransposedValuesOfFieldNamesGivesDifferentHashCodes()
        {
            Address address = new Address("_city", null, null);
            Address address2 = new Address(null, "_address1", null);

            Assert.That(address.GetHashCode() == address2.GetHashCode(), Is.False);
        }

        [Test]
        public void DerivedTypesHashCodesBehaveCorrectly()
        {
            ExpandedAddress address = new ExpandedAddress("Address99999", "Apt 123", "New Orleans", "LA");
            ExpandedAddress address2 = new ExpandedAddress("Address1", "Apt 123", "Austin", "TX");

            Assert.That(address.GetHashCode() == address2.GetHashCode(), Is.False);
        }
    }
}