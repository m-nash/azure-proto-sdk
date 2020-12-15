using NUnit.Framework;

namespace Azure.ResourceManager.Core.Tests
{
    public class LocationTests
    {
        [Test]
        public void CanCreateLocation()
        {
            Location tempLocation = new Location("West US");
            Assert.AreEqual("West US", tempLocation.Name);
        }

        [Test]
        public void EqualsToObjectTrue()
        {
            Location location1 = new Location("West US");
            Location location2 = new Location("West US");

            Assert.IsTrue(location1.Equals(location1));
            Assert.IsTrue(location1.Equals(location2));
        }

        [Test]
        public void EqualsToStringTrue()
        {
            Location location1 = new Location("West US");
            Assert.IsTrue(location1.Equals("West US"));
        }

        [Test]
        public void EqualsToObjectFalse()
        {
            Location location1 = new Location("West US");
            Location location2 = new Location("South US");

            Assert.IsFalse(location1.Equals(location2));
        }

        [Test]
        public void EqualsToStringFalse()
        {
            Location location1 = new Location("West US");
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
        public void CanCompareToLocationEquals()
        {
            Location location1 = new Location("South US");
            Assert.AreEqual(0, location1.CompareTo(location1));
        }

        [Test]
        public void CanCompareToStringEquals()
        {
            Location location1 = new Location("South US");
            Assert.AreEqual(0, location1.CompareTo("South US"));
        }

        [Test]
        public void CanCompareToLocationGreaterThan()
        {
            Location location1 = new Location("South US");
            Location location2 = new Location("East US");

            Assert.AreEqual(1, location1.CompareTo(location2));
        }

        [Test]
        public void CanCompareToStringGreaterThan()
        {
            Location location1 = new Location("South US");
            Assert.AreEqual(1, location1.CompareTo("East US"));
        }

        [Test]
        public void CanCompareToLocationLessThan()
        {
            Location location1 = new Location("South US");
            Location location3 = new Location("West US");

            Assert.AreEqual(-1, location1.CompareTo(location3));
        }

        [Test]
        public void CanCompareToStringLessThan()
        {
            Location location1 = new Location("South US");
            Assert.AreEqual(-1, location1.CompareTo("West US"));
        }

        [Test]
        public void CanCompareToNull()
        {
            string x = null;
            Location location1 = null;
            Assert.AreEqual(1, Location.Default.CompareTo(location1));
            Assert.AreEqual(1, Location.Default.CompareTo(x));
        }

        [Test]
        public void CanCastLocationToString()
        {
            string x = Location.Default;
            Assert.AreEqual(Location.Default.DisplayName, x);
        }

        [Test]
        public void CanCastStringToLocation()
        {
            Location location1 = "West US";
            Assert.AreEqual("West US", location1.DisplayName);
        }
    }
}
