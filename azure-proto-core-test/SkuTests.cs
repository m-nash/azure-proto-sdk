using azure_proto_core;
using NUnit.Framework;

namespace azure_proto_core_test
{
    class SkuTests
    {
        [Test]
        public void CompareToTests()
        {
            Sku sku1 = new Sku();
            Sku sku2 = null;

            sku1.Name = "name";
            sku1.Family = "family";
            sku1.Size = "size";
            sku1.Tier = "tier";
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku2 = new Sku();
            sku2.Name = "name";
            sku2.Family = "family";
            sku2.Size = "size";
            sku2.Tier = "tier";
            Assert.AreEqual(0, sku1.CompareTo(sku2));

            sku1.Capacity = 1;
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku2.Capacity = 2;
            Assert.AreEqual(-1, sku1.CompareTo(sku2));

            sku2.Capacity = 0;
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku2.Capacity = 1;
            Assert.AreEqual(0, sku1.CompareTo(sku2));

            sku1.Capacity = null;
            Assert.AreEqual(-1, sku1.CompareTo(sku2));

            sku1.Tier = "tier1";
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku2.Tier = "tier2";
            Assert.AreEqual(-1, sku1.CompareTo(sku2));

            sku2.Tier = null;
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku1.Size = "size1";
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku2.Size = "size2";
            Assert.AreEqual(-1, sku1.CompareTo(sku2));

            sku2.Size = null;
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku1.Family = "family1";
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku2.Family = "family2";
            Assert.AreEqual(-1, sku1.CompareTo(sku2));

            sku2.Family = null;
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku1.Name = "name1";
            Assert.AreEqual(1, sku1.CompareTo(sku2));

            sku2.Name = "name2";
            Assert.AreEqual(-1, sku1.CompareTo(sku2));

            sku2.Name = null;
            Assert.AreEqual(1, sku1.CompareTo(sku2));
        }

        [Test]
        public void EqualsTests()
        {
            Sku sku1 = new Sku();
            Sku sku2 = null;

            sku1.Name = "name";
            sku1.Family = "family";
            sku1.Size = "size";
            sku1.Tier = "tier";
            Assert.IsFalse(sku1.Equals(sku2));

            sku2 = new Sku();
            sku2.Name = "name";
            sku2.Family = "family";
            sku2.Size = "size";
            sku2.Tier = "tier";
            Assert.IsTrue(sku1.Equals(sku2));

            sku1.Capacity = 1;
            Assert.IsFalse(sku1.Equals(sku2));

            sku2.Capacity = 2;
            Assert.IsFalse(sku1.Equals(sku2));

            sku2.Capacity = 1;
            Assert.IsTrue(sku1.Equals(sku2));

            sku1.Capacity = null;
            Assert.IsFalse(sku1.Equals(sku2));

            sku1.Tier = "tier1";
            Assert.IsFalse(sku1.Equals(sku2));

            sku2.Tier = null;
            Assert.IsFalse(sku1.Equals(sku2));

            sku1.Size = "size1";
            Assert.IsFalse(sku1.Equals(sku2));

            sku2.Size = null;
            Assert.IsFalse(sku1.Equals(sku2));

            sku1.Family = "family1";
            Assert.IsFalse(sku1.Equals(sku2));

            sku2.Family = null;
            Assert.IsFalse(sku1.Equals(sku2));

            sku1.Name = "name1";
            Assert.IsFalse(sku1.Equals(sku2));

            sku2.Name = null;
            Assert.IsFalse(sku1.Equals(sku2));
        }
    }
}
