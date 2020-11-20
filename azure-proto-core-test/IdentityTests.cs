using azure_proto_core;
using NUnit.Framework;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace azure_proto_core_test
{
    public class IdentityTests : Identity
    {

        [TestCase]
        public void CheckNullResourceId()
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
            if (!isNull)
                identity2.ResourceId = resourceId2;
            Assert.AreEqual(1, identity1.CompareTo(identity2));
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

            Assert.IsTrue(identity1.Equals(identity2));

            Identity identity3 = new Identity();
            Identity identity4 = new Identity();
            Assert.IsTrue(identity1.Equals(identity2));
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

            Assert.IsFalse(identity1.Equals(identity2));

            Identity identity3 = null;
            Assert.IsFalse(identity1.Equals(identity3));
        }
         
        [TestCase]
        public void TestDeserializer()
        {
            string json = "";
            using (StreamReader f = new StreamReader("./TestAssets/UserAssignedMultipleIdentities.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            JsonElement rootElement = document.RootElement;
            var identityJsonProperty = rootElement.EnumerateObject().First<JsonProperty>();
            Identity back = Identity.DeserializeIdentity(identityJsonProperty.Value);
            Console.WriteLine(back);
            Assert.IsNotNull(back);
        }

        [TestCase]
        public void TestSerializer()
        {
            Identity identity1 = new Identity(); 
            identity1.ResourceId = "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport";
            identity1.ClientId = Guid.NewGuid();
            identity1.TenantId = Guid.NewGuid();
            identity1.PrincipalId = Guid.NewGuid();
            identity1.Kind = new IdentityKind("SystemAssigned");
            string value = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    identity1.Write(writer);
                    stream.Seek(0, SeekOrigin.Begin);
                    value = streamReader.ReadToEnd();
                }                
            }
            Console.WriteLine(value);
        }
    }
}
