using azure_proto_core;
using NUnit.Framework;

namespace azure_proto_core_test
{
    class PlanTests
    {
        [Test]
        public void CompareToTests()
        {
            Plan plan1 = new Plan();
            Plan plan2 = null;

            plan1.Name = "name";
            plan1.Product = "product";
            plan1.PromotionCode = "promotionCode";
            plan1.Publisher = "publisher";
            plan1.Version = "version";
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan2 = new Plan();
            plan2.Name = "name";
            plan2.Product = "product";
            plan2.PromotionCode = "promotionCode";
            plan2.Publisher = "publisher";
            plan2.Version = "version";
            Assert.AreEqual(0, plan1.CompareTo(plan2));

            plan1.Version = "version1";
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan2.Version = "version2";
            Assert.AreEqual(-1, plan1.CompareTo(plan2));

            plan2.Version = null;
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan1.Publisher = "publisher1";
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan2.Publisher = "publisher2";
            Assert.AreEqual(-1, plan1.CompareTo(plan2));

            plan2.Publisher = null;
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan1.PromotionCode = "promotionCode1";
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan2.PromotionCode = "promotionCode2";
            Assert.AreEqual(-1, plan1.CompareTo(plan2));

            plan2.PromotionCode = null;
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan1.Product = "product1";
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan2.Product = "product2";
            Assert.AreEqual(-1, plan1.CompareTo(plan2));

            plan2.Product = null;
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan1.Name = "name1";
            Assert.AreEqual(1, plan1.CompareTo(plan2));

            plan2.Name = "name2";
            Assert.AreEqual(-1, plan1.CompareTo(plan2));

            plan2.Name = null;
            Assert.AreEqual(1, plan1.CompareTo(plan2));
        }

        [Test]
        public void EqualsTests()
        {
            Plan plan1 = new Plan();
            Plan plan2 = null;

            plan1.Name = "name";
            plan1.Product = "product";
            plan1.PromotionCode = "promotionCode";
            plan1.Publisher = "publisher";
            plan1.Version = "version";
            Assert.IsFalse(plan1.Equals(plan2));

            plan2 = new Plan();
            plan2.Name = "name";
            plan2.Product = "product";
            plan2.PromotionCode = "promotionCode";
            plan2.Publisher = "publisher";
            plan2.Version = "version";
            Assert.IsTrue(plan1.Equals(plan2));

            plan1.Version = "version1";
            Assert.IsFalse(plan1.Equals(plan2));

            plan2.Version = null;
            Assert.IsFalse(plan1.Equals(plan2));

            plan1.Publisher = "publisher1";
            Assert.IsFalse(plan1.Equals(plan2));

            plan2.Publisher = null;
            Assert.IsFalse(plan1.Equals(plan2));

            plan1.PromotionCode = "promotionCode1";
            Assert.IsFalse(plan1.Equals(plan2));

            plan2.PromotionCode = null;
            Assert.IsFalse(plan1.Equals(plan2));

            plan1.Product = "product1";
            Assert.IsFalse(plan1.Equals(plan2));

            plan2.Product = null;
            Assert.IsFalse(plan1.Equals(plan2));

            plan1.Name = "name1";
            Assert.IsFalse(plan1.Equals(plan2));

            plan2.Name = null;
            Assert.IsFalse(plan1.Equals(plan2));
        }
    }
}
