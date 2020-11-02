using azure_proto_core;
using NUnit.Framework;
using System;

namespace azure_proto_core_test
{
    public class LocationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanCreateLocation()
        {
            Location tempLocation = new Location("West US");
            Assert.AreEqual("West US", tempLocation.Name);
            
        }

        [Test]
        public void EqualsToTrue()
        {
            Location location1 = new Location("West US");
            Location location2 = new Location("West US");

            Assert.IsTrue(location1.Equals(location1));
            Assert.IsTrue(location1.Equals(location2));

            Assert.IsTrue(location1.Equals("West US"));
        }

        [Test]
        public void EqualsToFalse()
        {
            Location location1 = new Location("West US");
            Location location2 = new Location("South US");

            Assert.IsFalse(location1.Equals(location2));
            Assert.IsFalse(location1.Equals("WestUS"));
        }

        [Test]
        public void CanParseToString()
        {
            Location location1 = new Location("West US");
            Assert.AreEqual("West US", location1.ToString());
            Assert.AreNotEqual("West Us", location1.ToString());
        }

        [Test]
        public void CanCompareToEquals()
        {
            Location location1 = new Location("South US");

            Assert.AreEqual(0, location1.CompareTo(location1));
            Assert.AreEqual(0, location1.CompareTo("South US"));

        }

        [Test]
        public void CanCompareToGreaterThan()
        {
            Location location1 = new Location("South US");
            Location location2 = new Location("East US");

            Assert.AreEqual(1, location1.CompareTo(location2));
            Assert.AreEqual(1, location1.CompareTo("East US"));
        }

        [Test]
        public void CanCompareToLessThan()
        {
            Location location1 = new Location("South US");
            Location location3 = new Location("West US");

            Assert.AreEqual(-1, location1.CompareTo(location3));
            Assert.AreEqual(-1, location1.CompareTo("West US"));
        }

        [TestCase (true)]
        public void CanCompareToNull(bool isNull)
        {
            string x = null;
            Assert.AreEqual(1, Location.Default.CompareTo(isNull ? null : Location.Default));
            Assert.AreEqual(1, Location.Default.CompareTo(x));

        }

        [Test]
        public void CanCastLocationToString()
        {
            string x = Location.Default;
            Assert.AreEqual(Location.Default.DisplayName,x);  
        }

        [Test]
        public void CanCastStringToLocation()
        {
            Location location1 = "West US";
            Assert.AreEqual("West US", location1.DisplayName);
        }
    }
}
