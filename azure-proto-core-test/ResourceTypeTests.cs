using azure_proto_core;
using NUnit.Framework;
using System;

namespace azure_proto_core_test
{
    public class ResourceTypeTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void CompareToZeroResourceType()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            Assert.AreEqual(0, resourceType1.CompareTo(resourceType2));

            resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            Assert.AreEqual(0, resourceType1.CompareTo(resourceType2));

            ResourceType resourceType3 = ResourceType.None;
            ResourceType resourceType4 = ResourceType.None;
            Assert.AreEqual(0, resourceType3.CompareTo(resourceType4));
        }

        [Test]
        public void CompareToOneResourceType()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101");
            Assert.AreEqual(1, resourceType1.CompareTo(resourceType2));

            ResourceType resourceType3 = ResourceType.None;
            Assert.AreEqual(1, resourceType1.CompareTo(resourceType3));
            Assert.AreEqual(1, resourceType2.CompareTo(resourceType3));
        }

        [Test]
        public void CompareToMinusOneResourceType()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/");         
            Assert.AreEqual(-1, resourceType1.CompareTo(resourceType2));

            ResourceType resourceType3 = ResourceType.None;
            Assert.AreEqual(-1, resourceType3.CompareTo(resourceType1));
            Assert.AreEqual(-1, resourceType3.CompareTo(resourceType2));
        }

        [TestCase]
        public void EqualsMethodTrueResourceType()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            Assert.AreEqual(true, resourceType1.Equals(resourceType2));

            resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            Assert.AreEqual(true, resourceType1.Equals(resourceType2));

            ResourceType resourceType3 = ResourceType.None;
            ResourceType resourceType4 = ResourceType.None;
            Assert.AreEqual(true, resourceType3.Equals(resourceType4));
        }

        [TestCase]
        public void EqualsMethodFalseResourceType()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/");
            Assert.AreEqual(false, resourceType1.Equals(resourceType2));

            ResourceType resourceType3 = ResourceType.None;
            Assert.AreEqual(false, resourceType3.Equals(resourceType1));
            Assert.AreEqual(false, resourceType3.Equals(resourceType2));
        }

        [Test]
        public void CompareToZeroString()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            Assert.AreEqual(0, resourceType1.CompareTo(resourceType2.ToString()));

            resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            Assert.AreEqual(0, resourceType1.CompareTo(resourceType2.ToString()));

            ResourceType resourceType3 = ResourceType.None;
            ResourceType resourceType4 = ResourceType.None;
            Assert.AreEqual(0, resourceType3.CompareTo(resourceType4.ToString()));
        }

        [Test]
        public void CompareToOneString()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101");
            Assert.AreEqual(1, resourceType1.CompareTo(resourceType2.ToString()));

            ResourceType resourceType3 = ResourceType.None;
            Assert.AreEqual(1, resourceType1.CompareTo(resourceType3.ToString()));
            Assert.AreEqual(1, resourceType2.CompareTo(resourceType3.ToString()));
        }

        [Test]
        public void CompareToMinusOneString()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/");
            Assert.AreEqual(-1, resourceType1.CompareTo(resourceType2.ToString()));

            ResourceType resourceType3 = ResourceType.None;
            Assert.AreEqual(-1, resourceType3.CompareTo(resourceType1.ToString()));
            Assert.AreEqual(-1, resourceType3.CompareTo(resourceType2.ToString()));
        }

        [TestCase]
        public void EqualsMethodTrueString()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport");
            Assert.AreEqual(true, resourceType1.Equals(resourceType2.ToString()));

            resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            Assert.AreEqual(true, resourceType1.Equals(resourceType2.ToString()));

            ResourceType resourceType3 = ResourceType.None;
            ResourceType resourceType4 = ResourceType.None;
            Assert.AreEqual(true, resourceType3.Equals(resourceType4.ToString()));
        }

        [TestCase]
        public void EqualsMethodFalseString()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test");
            ResourceType resourceType2 = new ResourceType("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/");
            Assert.AreEqual(false, resourceType1.Equals(resourceType2.ToString()));

            ResourceType resourceType3 = ResourceType.None;
            Assert.AreEqual(false, resourceType3.Equals(resourceType1.ToString()));
            Assert.AreEqual(false, resourceType3.Equals(resourceType2.ToString()));
        }
    }
}
