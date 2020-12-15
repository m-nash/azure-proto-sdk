using azure_proto_core;
using NUnit.Framework;
using System;

namespace azure_proto_core_test
{
    public class LocationTests
    {
        [Test]
        public void CanCreateLocation()
        {
            Location tempLocation = "West US";
            Assert.AreEqual("westus", tempLocation.Name);
        }

        [TestCase("USNorth")]
        [TestCase("Us West 12")]
        [TestCase("Us West 1a")]
        [TestCase(" Us West 1")]
        [TestCase("Us West 1 ")]
        [TestCase("")]
        [TestCase("*Us West")]
        [TestCase("Us *West")]
        [TestCase("Us West *")]
        public void NameTypeIsName(string location)
        {
            Location loc = location;
            Assert.IsTrue(loc.Name == loc.DisplayName && loc.Name == loc.CanonicalName);
        }

        [TestCase("us-west")]
        [TestCase("us-west-west")]
        [TestCase("us-west-2")]
        [TestCase("us-west-west-2")]
        [TestCase("a-b-c-5")]
        public void NameTypeIsCanonical(string location)
        {
            Location loc = location;
            Assert.IsTrue(loc.CanonicalName == location && loc.Name != location && loc.DisplayName != location);
        }

        [TestCase("Us West")]
        [TestCase("US West")]
        [TestCase("USa West")]
        [TestCase("West US")]
        [TestCase("West USa")]
        [TestCase("Us West West")]
        [TestCase("Us West 2")]
        [TestCase("Us West West 2")]
        [TestCase("A B C 5")]
        public void NameTypeIsDisplayName(string location)
        {
            Location loc = location;
            Assert.IsTrue(loc.DisplayName == location && loc.Name != location && loc.CanonicalName != location);
        }

        [TestCase(true, "West Us", "West Us")]
        [TestCase(true, "West Us", "WestUs")]
        [TestCase(false, "West Us", "WestUs2")]
        [TestCase(false, "West US", "")]
        [TestCase(false, "West US", "!#()@(#@")]
        [TestCase(false, "West US", "W3$t U$")]
        public void EqualsToObject(bool expected, string left, string right)
        {
            Location loc1 = left;
            Location loc2 = right;
            Assert.AreEqual(expected, loc1.Equals(loc2));
        }

        [TestCase(true, "West Us", "West Us")]
        [TestCase(true, "West Us", "WestUs")]
        [TestCase(false, "West Us", "WestUs2")]
        [TestCase(false, "West Us", "")]
        [TestCase(false, "West Us", "!#()@(#@")]
        [TestCase(false, "West Us", "W3$t U$")]
        public void EqualsToString(bool expected, string left, string right)
        {
            Location location = left;
            Assert.AreEqual(expected, location.Equals(right));
        }

        [TestCase("West Us", null)]
        public void EqualsToArgumentNull(string left, string right)
        {
            Location location1 = left;
            Assert.Throws<ArgumentNullException>(() => location1.Equals(right));
        }

        [Test]
        public void CanParseToString()
        {
            Location location1 = "West US";
            Assert.AreEqual("West US", location1.ToString());
            Assert.AreNotEqual("West Us", location1.ToString());
        }

        [Test]
        public void CanCompareToLocationEquals()
        {
            Location location1 = "South US";
            Assert.AreEqual(0, location1.CompareTo(location1));
        }

        [Test]
        public void CanCompareToStringEquals()
        {
            Location location1 = "South US";
            Assert.AreEqual(0, location1.CompareTo("South US"));
        }

        [Test]
        public void CanCompareToLocationGreaterThan()
        {
            Location location1 = "South US";
            Location location2 = "East US";

            Assert.AreEqual(1, location1.CompareTo(location2));
        }

        [Test]
        public void CanCompareToStringGreaterThan()
        {
            Location location1 = "South US";
            Assert.AreEqual(1, location1.CompareTo("East US"));
        }

        [Test]
        public void CanCompareToLocationLessThan()
        {
            Location location1 = "South US";
            Location location3 = "West US";

            Assert.AreEqual(-1, location1.CompareTo(location3));
        }

        [Test]
        public void CanCompareToStringLessThan()
        {
            Location location1 = "South US";
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
