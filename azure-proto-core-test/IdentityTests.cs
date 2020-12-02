using azure_proto_core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
            Guid principalId = Guid.NewGuid();
            identity1.TenantId = tenantId;
            identity1.PrincipalId = principalId;
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
            Guid principalId = Guid.NewGuid();
            identity1.TenantId = tenantId;
            identity1.PrincipalId = principalId;
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
        public void TestSerializerSystemAssigned()
        {
            Identity identity1 = new Identity(); 
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
            string actual = "";
            using (StreamReader f = new StreamReader("./TestAssets/SystemAssigned.json"))
                actual = f.ReadToEnd();
            actual = actual.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            Assert.AreEqual(value, actual);
        }

        [TestCase]
        public void TestSerializerUserAssigned()
        {
            Identity identity1 = new Identity();
            identity1.ResourceId = "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport";
            identity1.Kind = new IdentityKind("UserAssigned");
            Dictionary<string, azure_proto_core.Resources.UserAssignedIdentity.ClientAndPrincipalId> dict = new Dictionary<string, azure_proto_core.Resources.UserAssignedIdentity.ClientAndPrincipalId>();
            var userClientAndPrincipalId = new azure_proto_core.Resources.UserAssignedIdentity.ClientAndPrincipalId("test1", "test2");
            dict.Add("/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity", userClientAndPrincipalId);
            identity1.UserAssignedIdentities = dict;
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
            string actual = "";
            using (StreamReader f = new StreamReader("./TestAssets/UserAssigned.json"))
                actual = f.ReadToEnd();
            Assert.AreEqual(value, actual);
        }

        [TestCase]
        public void TestSerializerUserAssignedMultipleIdentities()
        {
            Identity identity1 = new Identity();
            identity1.ResourceId = "/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport";
            identity1.Kind = new IdentityKind("UserAssigned");
            Dictionary<string, azure_proto_core.Resources.UserAssignedIdentity.ClientAndPrincipalId> dict = new Dictionary<string, azure_proto_core.Resources.UserAssignedIdentity.ClientAndPrincipalId>();
            var userClientAndPrincipalId = new azure_proto_core.Resources.UserAssignedIdentity.ClientAndPrincipalId("test1", "test2");
            var userClientAndPrincipalId2 = new azure_proto_core.Resources.UserAssignedIdentity.ClientAndPrincipalId("test3", "test4");
            dict.Add("/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity", userClientAndPrincipalId);
            dict.Add("/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity2", userClientAndPrincipalId2);

            identity1.UserAssignedIdentities = dict;
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
            string actual = "";
            using (StreamReader f = new StreamReader("./TestAssets/UserAssignedMultipleIdentities.json"))
                actual = f.ReadToEnd();
            Assert.AreEqual(value, actual);
        }
    }
}
