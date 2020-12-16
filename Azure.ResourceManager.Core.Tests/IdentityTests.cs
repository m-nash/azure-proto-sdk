using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Azure.ResourceManager.Core.Tests
{
    public class IdentityTests
    {
        [TestCase]
        public void CheckNoParamConstructor()
        {
            Identity identity = new Identity();
            Assert.IsNotNull(identity);
            Assert.IsTrue(identity.UserAssignedIdentities.Count == 0);
            Assert.IsNull(identity.SystemAssignedIdentity);
        }

        [TestCase]
        public void CheckUserTrueConstructor()
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity = new Identity(dict1, true);
            Assert.IsNotNull(identity);
            Assert.IsNotNull(identity.UserAssignedIdentities);
            Assert.IsTrue(identity.UserAssignedIdentities.Count == 1);
            Assert.IsNotNull(identity.SystemAssignedIdentity);
            Assert.IsNull(identity.SystemAssignedIdentity.TenantId);
            Assert.IsNull(identity.SystemAssignedIdentity.PrincipalId);
        }

        [TestCase]
        public void CheckUserFalseConstructor()
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity = new Identity(system, dict1);
            Assert.IsNotNull(identity);
            Assert.IsNotNull(identity.UserAssignedIdentities);
            Assert.IsTrue(identity.UserAssignedIdentities.Count == 1);
            Assert.IsNotNull(identity.SystemAssignedIdentity);
            Assert.IsTrue(identity.SystemAssignedIdentity.TenantId.Equals(Guid.Empty));
            Assert.IsTrue(identity.SystemAssignedIdentity.PrincipalId.Equals(Guid.Empty));
        }

        [TestCase]
        public void EqualsNullOtherTestFalse()
        {
            Identity identity = new Identity();
            Identity other = null;
            Assert.IsFalse(identity.Equals(other));
        }

        [TestCase]
        public void EqualsNullOtherTest()
        {
            Identity identity = new Identity();
            Identity other = new Identity();
            Assert.IsTrue(identity.Equals(other));
        }

        [TestCase]
        public void EqualsReferenceTestTrue()
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity = new Identity(system, dict1);
            Identity identity1 = identity;
            Assert.IsTrue(identity.Equals(identity1));
        }

        [TestCase]
        public void EqualsTestTrue()
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity = new Identity(system, dict1);
            var dict2 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict2["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system2 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity1 = new Identity(system2, dict2);
            Assert.IsTrue(identity.Equals(identity1));
        }

        [TestCase]
        public void EqualsTestFalse()
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity = new Identity(system, dict1);
            var dict2 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict2["/subscriptions/6b085460-5f21-475e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system2 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity1 = new Identity(system2, dict2);
            Assert.IsFalse(identity.Equals(identity1));
        }

        [TestCase]
        public void TestSerializerValidSystemAndUser()
        {
            SystemAssignedIdentity systemAssignedIdentity = new SystemAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = userAssignedIdentity;
            Identity identity = new Identity(systemAssignedIdentity, dict1);
            string system = "{\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\",\"tenantId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\"}";
            string user = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}";
            string actual = "{\"identity\":" +
                system + "," +
                "\"kind\":\"SystemAssigned, UserAssigned\"," +
                "\"userAssignedIdentities\":" +
                "{" + "\"/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport\":" +
                user + "}}";
            string value = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    value = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(actual, value);
        }

        [TestCase]
        public void TestSerializerValidSystemOnly()
        {
            SystemAssignedIdentity systemAssignedIdentity = new SystemAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            Identity identity = new Identity(systemAssignedIdentity, null);
            string system = "{\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\",\"tenantId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\"}";
            string actual = "{\"identity\":" +
                system + "," +
                "\"kind\":\"SystemAssigned\"}";
            string value = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    value = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(actual, value);
        }

        [TestCase]
        public void TestSerializerValidUserEmptySystem()
        {
            UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = userAssignedIdentity;
            Identity identity = new Identity(dict1, true);
            string system = "{\"principalId\":\"null\",\"tenantId\":\"null\"}";
            string user = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}";
            string actual = "{\"identity\":" +
                system + "," +
                "\"kind\":\"SystemAssigned, UserAssigned\"," +
                "\"userAssignedIdentities\":" +
                "{" + "\"/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport\":" +
                user + "}}";
            string value = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    value = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(actual, value);
        }

        [TestCase]
        public void TestSerializerValidUserNullSystem()
        {
            UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = userAssignedIdentity;
            Identity identity = new Identity(dict1, false);
            string user = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}";
            string actual = "{\"identity\":{" +
                "\"kind\":\"UserAssigned\"," +
                "\"userAssignedIdentities\":" +
                "{" + "\"/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport\":" +
                user + "}}";
            string value = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    value = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(actual, value);
        }
    }
}
