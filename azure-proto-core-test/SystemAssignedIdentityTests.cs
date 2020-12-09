using azure_proto_core.Resources;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace azure_proto_core_test
{
    public class SystemAssignedIdentityTests : SystemAssignedIdentity
    {

       [TestCase(null, null, null, null, true)] 
       [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", false)] 
       public void CompareToZero(string tenantId1, string principalId1, string tenantId2, string principalId2, bool isNull)
        {
            SystemAssignedIdentity identity1;
            SystemAssignedIdentity identity2;
            if (isNull)
            {
                identity1 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
                identity2 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            }

            else
            {
                identity1 = new SystemAssignedIdentity(new Guid(tenantId1), new Guid(principalId1));
                identity2 = new SystemAssignedIdentity(new Guid(tenantId2), new Guid(principalId2));
            }
            
            Assert.AreEqual(0, identity1.CompareTo(identity2));
        }

        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", null, null, true)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db46", "de29bab1-49e1-4705-819b-4dfddceaaa97", false)]
        public void CompareToOne(string tenantId1, string principalId1, string tenantId2, string principalId2, bool isNull)
        {
            SystemAssignedIdentity identity1 = new SystemAssignedIdentity(new Guid(tenantId1), new Guid(principalId1));
            SystemAssignedIdentity identity2;
            if (isNull)
            {
                identity2 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            }

            else
            {
                identity2 = new SystemAssignedIdentity(new Guid(tenantId2), new Guid(principalId2));
            }

            Assert.AreEqual(1, identity1.CompareTo(identity2));
        }

        [TestCase(null, null, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98",  true)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db48", "de29bab1-49e1-4705-819b-4dfddceaaa99", false)]
        public void CompareToMinusOne(string tenantId1, string principalId1, string tenantId2, string principalId2, bool isNull)
        {
            SystemAssignedIdentity identity1;
            SystemAssignedIdentity identity2 = new SystemAssignedIdentity(new Guid(tenantId2), new Guid(principalId2));
            if (isNull)
            {
                identity1 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            }

            else
            {
                identity1 = new SystemAssignedIdentity(new Guid(tenantId1), new Guid(principalId1));
            }

            Assert.AreEqual(-1, identity1.CompareTo(identity2));
        }

        [TestCase(null, null, null, null, true)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", false)]
        public void EqualsMethodTrue(string tenantId1, string principalId1, string tenantId2, string principalId2, bool isNull)
        {
            SystemAssignedIdentity identity1;
            SystemAssignedIdentity identity2;
            if (isNull)
            {
                identity1 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
                identity2 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            }

            else
            {
                identity1 = new SystemAssignedIdentity(new Guid(tenantId1), new Guid(principalId1));
                identity2 = new SystemAssignedIdentity(new Guid(tenantId2), new Guid(principalId2));
            }

            Assert.IsTrue(identity1.Equals(identity2));
        }

        [TestCase(null, null, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", true, false)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db44", "de29bab1-49e1-4705-819b-4dfddceaaa94", false, false)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", null, null, false, true)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa93", "72f988bf-86f1-41af-91ab-2d7cd011db42", "de29bab1-49e1-4705-819b-4dfddceaaa91", false, false)]
        public void EqualsMethodFalse(string tenantId1, string principalId1, string tenantId2, string principalId2, bool isNull1, bool isNull2)
        {
            SystemAssignedIdentity identity1;
            SystemAssignedIdentity identity2;
            if (isNull1)
            {
                identity1 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
                identity2 = new SystemAssignedIdentity(new Guid(tenantId2), new Guid(principalId2));
            }
            else if (isNull2)
            {
                identity1 = new SystemAssignedIdentity(new Guid(tenantId1), new Guid(principalId1));
                identity2 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            }
            else
            {
                identity1 = new SystemAssignedIdentity(new Guid(tenantId1), new Guid(principalId1));
                identity2 = new SystemAssignedIdentity(new Guid(tenantId2), new Guid(principalId2));
            }

            Assert.IsFalse(identity1.Equals(identity2));
        }

        [TestCase]
        public void TestDeserializerValid()
        {
            string json = "";
            using (StreamReader f = new StreamReader("C:/GitHub/me/azure-proto-sdk/azure-proto-core-test/TestAssets/SystemAssignedIdentity/SystemAssignedValid.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            JsonElement rootElement = document.RootElement;
            var identityJsonProperty = rootElement.EnumerateObject().First<JsonProperty>();
            SystemAssignedIdentity back = SystemAssignedIdentity.Deserialize(identityJsonProperty.Value);
            Assert.IsTrue("de29bab1-49e1-4705-819b-4dfddceaaa98".Equals(back.PrincipalId.ToString()));
            Assert.IsTrue("72f988bf-86f1-41af-91ab-2d7cd011db47".Equals(back.TenantId.ToString()));
        }

        [TestCase]
        public void TestDeserializerValidExtraField()
        {
            string json = "";
            using (StreamReader f = new StreamReader("C:/GitHub/me/azure-proto-sdk/azure-proto-core-test/TestAssets/SystemAssignedIdentity/SystemAssignedValidExtraField.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            JsonElement rootElement = document.RootElement;
            var identityJsonProperty = rootElement.EnumerateObject().ElementAt<JsonProperty>(1);
            SystemAssignedIdentity back = SystemAssignedIdentity.Deserialize(identityJsonProperty.Value);
            Assert.IsTrue("de29bab1-49e1-4705-819b-4dfddceaaa98".Equals(back.PrincipalId.ToString()));
            Assert.IsTrue("72f988bf-86f1-41af-91ab-2d7cd011db47".Equals(back.TenantId.ToString()));
        }

        [TestCase]
        public void TestDeserializerBothValuesNull()
        {
            string json = "";
            using (StreamReader f = new StreamReader("C:/GitHub/me/azure-proto-sdk/azure-proto-core-test/TestAssets/SystemAssignedIdentity/SystemAssignedBothValuesNull.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            JsonElement rootElement = document.RootElement;
            var identityJsonProperty = rootElement.EnumerateObject().First<JsonProperty>();
            var back = SystemAssignedIdentity.Deserialize(identityJsonProperty.Value);
            Assert.IsNull(back);
        }

        [TestCase]
        public void TestDeserializerBothEmptyString()
        {
            string json = "";
            using (StreamReader f = new StreamReader("C:/GitHub/me/azure-proto-sdk/azure-proto-core-test/TestAssets/SystemAssignedIdentity/SystemAssignedBothEmptyString.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            JsonElement rootElement = document.RootElement;
            var identityJsonProperty = rootElement.EnumerateObject().First<JsonProperty>();
            Console.WriteLine(Directory.GetCurrentDirectory());
            Assert.Throws<FormatException>(delegate { SystemAssignedIdentity.Deserialize(identityJsonProperty.Value); });
        }

        /*[TestCase]
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
        }*/
    }
}
