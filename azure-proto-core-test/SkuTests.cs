using azure_proto_core;
using NUnit.Framework;

namespace azure_proto_core_test
{
    class SkuTests
    {
        [TestCase(0, "name", "name")]
        [TestCase(1, "Name", "name")]
        [TestCase(0, null, null)]
        [TestCase(1, "name", null)]
        [TestCase(-1, null, "name")]
        [TestCase(0, "${?/>._`", "${?/>._`")]
        [TestCase(1, "${?/>._`", "")]
        public void CompareToName(int expected, string name1, string name2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Name = name1;
            sku2.Name = name2;
            Assert.AreEqual(expected, sku1.CompareTo(sku2));
        }

        [TestCase(0, "family", "family")]
        [TestCase(1, "Family", "family")]
        [TestCase(0, null, null)]
        [TestCase(1, "family", null)]
        [TestCase(-1, null, "family")]
        [TestCase(0, "${?/>._`", "${?/>._`")]
        [TestCase(1, "${?/>._`", "")]
        public void CompareToFamily(int expected, string family1, string family2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Family = family1;
            sku2.Family = family2;
            Assert.AreEqual(expected, sku1.CompareTo(sku2));
        }

        [TestCase(0, "size", "size")]
        [TestCase(1, "Size", "size")]
        [TestCase(0, null, null)]
        [TestCase(1, "size", null)]
        [TestCase(-1, null, "size")]
        [TestCase(0, "${?/>._`", "${?/>._`")]
        [TestCase(1, "${?/>._`", "")]
        public void CompareToSize(int expected, string size1, string size2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Size = size1;
            sku2.Size = size2;
            Assert.AreEqual(expected, sku1.CompareTo(sku2));
        }

        [TestCase(0, "tier", "tier")]
        [TestCase(1, "Tier", "tier")]
        [TestCase(0, null, null)]
        [TestCase(1, "tier", null)]
        [TestCase(-1, null, "tier")]
        [TestCase(0, "${?/>._`", "${?/>._`")]
        [TestCase(1, "${?/>._`", "")]
        public void CompareToTier(int expected, string tier1, string tier2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Tier = tier1;
            sku2.Tier = tier2;
            Assert.AreEqual(expected, sku1.CompareTo(sku2));
        }

        [TestCase(0, 1, 1)]
        [TestCase(1, 1, -1)]
        [TestCase(0, null, null)]
        [TestCase(1, -1, null)]
        [TestCase(-1, null, 1)]
        public void CompareToCapacity(int expected, long? capacity1, long? capacity2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Capacity = capacity1;
            sku2.Capacity = capacity2;
            Assert.AreEqual(expected, sku1.CompareTo(sku2));
        }

        [Test]
        public void CompareToNullSku()
        {
            Sku sku1 = new Sku();
            Sku sku2 = null;
            Assert.AreEqual(1, sku1.CompareTo(sku2));
        }

        [TestCase(true, "name", "name")]
        [TestCase(false, "Name", "name")]
        [TestCase(true, null, null)]
        [TestCase(false, "name", null)]
        [TestCase(false, null, "name")]
        [TestCase(true, "${?/>._`", "${?/>._`")]
        [TestCase(false, "${?/>._`", "")]
        public void EqualsToName(bool expected, string name1, string name2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Name = name1;
            sku2.Name = name2;
            if (expected)
            {
                Assert.IsTrue(sku1.Equals(sku2));
            }
            else
            {
                Assert.IsFalse(sku1.Equals(sku2));
            }
        }

        [TestCase(true, "family", "family")]
        [TestCase(false, "Family", "family")]
        [TestCase(true, null, null)]
        [TestCase(false, "family", null)]
        [TestCase(false, null, "family")]
        [TestCase(true, "${?/>._`", "${?/>._`")]
        [TestCase(false, "${?/>._`", "")]
        public void EqualsToFamily(bool expected, string family1, string family2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Family = family1;
            sku2.Family = family2;
            if (expected)
            {
                Assert.IsTrue(sku1.Equals(sku2));
            }
            else
            {
                Assert.IsFalse(sku1.Equals(sku2));
            }
        }

        [TestCase(true, "size", "size")]
        [TestCase(false, "Size", "size")]
        [TestCase(true, null, null)]
        [TestCase(false, "size", null)]
        [TestCase(false, null, "size")]
        [TestCase(true, "${?/>._`", "${?/>._`")]
        [TestCase(false, "${?/>._`", "")]
        public void EqualsToSize(bool expected, string size1, string size2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Size = size1;
            sku2.Size = size2;
            if (expected)
            {
                Assert.IsTrue(sku1.Equals(sku2));
            }
            else
            {
                Assert.IsFalse(sku1.Equals(sku2));
            }
        }

        [TestCase(true, "tier", "tier")]
        [TestCase(false, "Tier", "tier")]
        [TestCase(true, null, null)]
        [TestCase(false, "tier", null)]
        [TestCase(false, null, "tier")]
        [TestCase(true, "${?/>._`", "${?/>._`")]
        [TestCase(false, "${?/>._`", "")]
        public void EqualsToTier(bool expected, string tier1, string tier2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Tier = tier1;
            sku2.Tier = tier2;
            if (expected)
            {
                Assert.IsTrue(sku1.Equals(sku2));
            }
            else
            {
                Assert.IsFalse(sku1.Equals(sku2));
            }
        }

        [TestCase(true, 1, 1)]
        [TestCase(false, 1, 0)]
        [TestCase(true, null, null)]
        [TestCase(false, 1, null)]
        [TestCase(false, null, 1)]
        public void EqualsToCapacity(bool expected, long? capacity1, long? capacity2)
        {
            Sku sku1 = new Sku();
            Sku sku2 = new Sku();
            sku1.Capacity = capacity1;
            sku2.Capacity = capacity2;
            if (expected)
            {
                Assert.IsTrue(sku1.Equals(sku2));
            }
            else
            {
                Assert.IsFalse(sku1.Equals(sku2));
            }
        }

        [Test]
        public void EqualsToNullSku()
        {
            Sku sku1 = new Sku();
            Sku sku2 = null;
            Assert.IsFalse(sku1.Equals(sku2));
        }

        [Test]
        public void EqualsToObject()
        {
            Sku sku1 = new Sku();
            object sku2 = "random";
            Assert.IsFalse(sku1.Equals(sku2));
        }
    }
}
