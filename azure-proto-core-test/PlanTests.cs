using azure_proto_core;
using NUnit.Framework;

namespace azure_proto_core_test
{
    class PlanTests
    {
        [TestCase(0, "name", "name")]
        [TestCase(0, "Name", "name")]
        [TestCase(0, null, null)]
        [TestCase(1, "name", null)]
        [TestCase(-1, null, "name")]
        [TestCase(0, "${?/>._`", "${?/>._`")]
        [TestCase(1, "${?/>._`", "")]
        public void CompareToName(int expected, string name1, string name2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.Name = name1;
            plan2.Name = name2;
            Assert.AreEqual(expected, plan1.CompareTo(plan2));
        }

        [TestCase(0, "product", "product")]
        [TestCase(0, "Product", "product")]
        [TestCase(0, null, null)]
        [TestCase(1, "product", null)]
        [TestCase(-1, null, "product")]
        [TestCase(0, "${?/>._`", "${?/>._`")]
        [TestCase(1, "${?/>._`", "")]
        public void CompareToProduct(int expected, string product1, string product2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.Product = product1;
            plan2.Product = product2;
            Assert.AreEqual(expected, plan1.CompareTo(plan2));
        }

        [TestCase(0, "promotionCode", "promotionCode")]
        [TestCase(0, "PromotionCode", "promotionCode")]
        [TestCase(0, null, null)]
        [TestCase(1, "promotionCode", null)]
        [TestCase(-1, null, "promotionCode")]
        [TestCase(0, "${?/>._`", "${?/>._`")]
        [TestCase(1, "${?/>._`", "")]
        public void CompareToPromotionCode(int expected, string promotionCode1, string promotionCode2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.PromotionCode = promotionCode1;
            plan2.PromotionCode = promotionCode2;
            Assert.AreEqual(expected, plan1.CompareTo(plan2));
        }

        [TestCase(0, "publisher", "publisher")]
        [TestCase(0, "Publisher", "publisher")]
        [TestCase(0, null, null)]
        [TestCase(1, "publisher", null)]
        [TestCase(-1, null, "publisher")]
        [TestCase(0, "${?/>._`", "${?/>._`")]
        [TestCase(1, "${?/>._`", "")]
        public void CompareToPublisher(int expected, string publisher1, string publisher2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.Publisher = publisher1;
            plan2.Publisher = publisher2;
            Assert.AreEqual(expected, plan1.CompareTo(plan2));
        }

        [TestCase(0, "version", "version")]
        [TestCase(0, "Version", "version")]
        [TestCase(0, null, null)]
        [TestCase(1, "version", null)]
        [TestCase(-1, null, "version")]
        [TestCase(0, "${?/>._`", "${?/>._`")]
        [TestCase(1, "${?/>._`", "")]
        public void CompareToVersion(int expected, string version1, string version2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.Version = version1;
            plan2.Version = version2;
            Assert.AreEqual(expected, plan1.CompareTo(plan2));
        }

        [Test]
        public void CompareToNullPlan()
        {
            Plan plan1 = new Plan();
            Plan plan2 = null;
            Assert.AreEqual(1, plan1.CompareTo(plan2));
        }

        [Test]
        public void CompareToSamePlans()
        {
            Plan plan1 = new Plan();
            Plan plan2 = plan1;
            Assert.AreEqual(0, plan1.CompareTo(plan2));
        }

        [TestCase(1, "Nameb", "namea", "versiona", "Versionb")]
        [TestCase(1, "Nameb", "namea", "versiona", "versiona")]
        [TestCase(-1, "namea", "Nameb", "Versionb", "versiona")]
        public void CompareToMore(int expected, string name1, string name2, string version1, string version2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.Name = name1;
            plan2.Name = name2;
            plan1.Version = version1;
            plan2.Version = version2;
            Assert.AreEqual(expected, plan1.CompareTo(plan2));
        }

        [TestCase(true, "name", "name")]
        [TestCase(true, "Name", "name")]
        [TestCase(true, null, null)]
        [TestCase(false, "name", null)]
        [TestCase(false, null, "name")]
        [TestCase(true, "${?/>._`", "${?/>._`")]
        [TestCase(false, "${?/>._`", "")]
        public void EqualsToName(bool expected, string name1, string name2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.Name = name1;
            plan2.Name = name2;
            if (expected)
            {
                Assert.IsTrue(plan1.Equals(plan2));
            }
            else
            {
                Assert.IsFalse(plan1.Equals(plan2));
            }
        }

        [TestCase(true, "product", "product")]
        [TestCase(true, "Product", "product")]
        [TestCase(true, null, null)]
        [TestCase(false, "product", null)]
        [TestCase(false, null, "product")]
        [TestCase(true, "${?/>._`", "${?/>._`")]
        [TestCase(false, "${?/>._`", "")]
        public void EqualsToProduct(bool expected, string product1, string product2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.Product = product1;
            plan2.Product = product2;
            if (expected)
            {
                Assert.IsTrue(plan1.Equals(plan2));
            }
            else
            {
                Assert.IsFalse(plan1.Equals(plan2));
            }
        }

        [TestCase(true, "promotionCode", "promotionCode")]
        [TestCase(true, "PromotionCode", "promotionCode")]
        [TestCase(true, null, null)]
        [TestCase(false, "promotionCode", null)]
        [TestCase(false, null, "promotionCode")]
        [TestCase(true, "${?/>._`", "${?/>._`")]
        [TestCase(false, "${?/>._`", "")]
        public void EqualsToPromotionCode(bool expected, string promotionCode1, string promotionCode2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.PromotionCode = promotionCode1;
            plan2.PromotionCode = promotionCode2;
            if (expected)
            {
                Assert.IsTrue(plan1.Equals(plan2));
            }
            else
            {
                Assert.IsFalse(plan1.Equals(plan2));
            }
        }

        [TestCase(true, "publisher", "publisher")]
        [TestCase(true, "Publisher", "publisher")]
        [TestCase(true, null, null)]
        [TestCase(false, "publisher", null)]
        [TestCase(false, null, "publisher")]
        [TestCase(true, "${?/>._`", "${?/>._`")]
        [TestCase(false, "${?/>._`", "")]
        public void EqualsToPublisher(bool expected, string publisher1, string publisher2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.Publisher = publisher1;
            plan2.Publisher = publisher2;
            if (expected)
            {
                Assert.IsTrue(plan1.Equals(plan2));
            }
            else
            {
                Assert.IsFalse(plan1.Equals(plan2));
            }
        }

        [TestCase(true, "version", "version")]
        [TestCase(true, "Version", "version")]
        [TestCase(true, null, null)]
        [TestCase(false, "version", null)]
        [TestCase(false, null, "version")]
        [TestCase(true, "${?/>._`", "${?/>._`")]
        [TestCase(false, "${?/>._`", "")]
        public void EqualsToVersion(bool expected, string version1, string version2)
        {
            Plan plan1 = new Plan();
            Plan plan2 = new Plan();
            plan1.Version = version1;
            plan2.Version = version2;
            if (expected)
            {
                Assert.IsTrue(plan1.Equals(plan2));
            }
            else
            {
                Assert.IsFalse(plan1.Equals(plan2));
            }
        }

        [Test]
        public void EqualsToNullPlan()
        {
            Plan plan1 = new Plan();
            Plan plan2 = null;
            Assert.IsFalse(plan1.Equals(plan2));
        }

        [Test]
        public void EqualsToObject()
        {
            Plan plan1 = new Plan();
            object plan2 = "random";
            Assert.IsFalse(plan1.Equals(plan2));
        }

        [Test]
        public void EqualsToSamePlans()
        {
            Plan plan1 = new Plan();
            Plan plan2 = plan1;
            Assert.IsTrue(plan1.Equals(plan2));
        }
    }
}
