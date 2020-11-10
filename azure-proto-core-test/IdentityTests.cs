using azure_proto_core;
using NUnit.Framework;
using System;

namespace azure_proto_core_test
{
    public class IdentityTests
    {
        
        [TestCase]
        public void CheckNullProperty()
        {
            Identity identity1 = new Identity();
            identity1.ResourceId = "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport";            
            Assert.IsNotNull(identity1.ResourceId);
            Identity identity2 = new Identity();
            Assert.IsNull(identity2.ResourceId);
        }
        
        [TestCase("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport", "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport")]
        [TestCase("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101", "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101")]
        [TestCase("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test", "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test")]
        public void CompareToZero(string resourceId1, string resourceId2)
        {
            Identity identity1 = new Identity();
            identity1.ResourceId = resourceId1;
            Identity identity2 = new Identity();
            identity2.ResourceId = resourceId2;
            Assert.AreEqual(0, identity1.CompareTo(identity2));
        }

        [TestCase("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport", "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101", false)]
        [TestCase("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101", null, true)]
        public void CompareToOne(string resourceId1, string resourceId2, bool isNull)
        {
            Identity identity1 = new Identity();
            identity1.ResourceId = resourceId1;
            Identity identity2 = new Identity();
            identity2.ResourceId = resourceId2;
            Assert.AreEqual(1, identity1.CompareTo(isNull?null: identity2));           
        }

        [TestCase("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101", "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test")]
        [TestCase("/subscriptions/6b085460-5f21-477e-ba44-1035046e9101", "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport")]
        public void CompareToMinusOne(string resourceId1, string resourceId2)
        {
            Identity identity1 = new Identity();
            identity1.ResourceId = resourceId1;
            Identity identity2 = new Identity();
            identity2.ResourceId = resourceId2;
            Assert.AreEqual(-1, identity1.CompareTo(identity2));
        }

        [TestCase]
        public void EqualsMethodTrue()
        {
            Identity identity1 = new Identity();
            Identity identity2 = new Identity();
            
            identity1.ResourceId = "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport";
            identity2.ResourceId = "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport";

            Guid tenantId = Guid.NewGuid();
            Guid clientId = Guid.NewGuid();
            Guid principalId = Guid.NewGuid();
            identity1.ClientId = clientId;
            identity1.TenantId = tenantId;
            identity1.PrincipalId = principalId;
            identity2.ClientId = clientId;
            identity2.TenantId = tenantId;
            identity2.PrincipalId = principalId;
            identity1.Kind = new IdentityKind("test");
            identity2.Kind = new IdentityKind("test");

            Assert.AreEqual(identity1.Equals(identity2), true);

            Identity identity3 = new Identity();
            Identity identity4 = new Identity();
            Assert.AreEqual(identity1.Equals(identity2), true);
        }

        [TestCase]
        public void EqualsMethodFalse()
        {
            Identity identity1 = new Identity();
            Identity identity2 = new Identity();

            identity1.ResourceId = "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport";
            identity2.ResourceId = "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport";

            Guid tenantId = Guid.NewGuid();
            Guid clientId = Guid.NewGuid();
            Guid principalId = Guid.NewGuid();
            identity1.ClientId = clientId;
            identity1.TenantId = tenantId;
            identity1.PrincipalId = principalId;
            identity2.ClientId = clientId;
            identity2.TenantId = tenantId;
            identity2.PrincipalId = Guid.NewGuid();

            Assert.AreEqual(identity1.Equals(identity2), false);

            Identity identity3 = null;
            Assert.AreEqual(identity1.Equals(identity3), false);
        }
    }
}
