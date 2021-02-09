using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [TestCase ("/subscriptions/6b085460-5f00-477e-ba44-1035046e9101/resourceGroups/tester/providers/Microsoft.Web/sites/autotest", false)]
        [TestCase ("", true)]
        [TestCase (" ", true)]
        [TestCase (null, true)]
        public void CheckUserTrueConstructor(string resourceID, bool invalidParameter)
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();

            if (invalidParameter)
            {
                Assert.Throws<System.ArgumentOutOfRangeException>(() => { dict1[resourceID] = new UserAssignedIdentity(Guid.Empty, Guid.Empty); });
            }
            else
            {
                dict1[resourceID] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
                Identity identity = new Identity(dict1, true);
                Assert.IsNotNull(identity);
                Assert.IsNotNull(identity.UserAssignedIdentities);
                Assert.IsTrue(identity.UserAssignedIdentities.Count == 1);
                Assert.IsNotNull(identity.SystemAssignedIdentity);
                Assert.IsNull(identity.SystemAssignedIdentity.TenantId);
                Assert.IsNull(identity.SystemAssignedIdentity.PrincipalId);
            }
        }

        [TestCase ("/subscriptions/6b085460-5f00-477e-ba44-1035046e9101/resourceGroups/tester/providers/Microsoft.Web/sites/autotest", false)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase(null, true)]
        public void CheckUserFalseConstructor(string resourceID, bool invalidParameter)
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();

            if(invalidParameter)
            {
                Assert.Throws<System.ArgumentOutOfRangeException>(() => { dict1[resourceID] = new UserAssignedIdentity(Guid.Empty, Guid.Empty); });
            }
            else
            {
                dict1[resourceID] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
                var system = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
                Identity identity = new Identity(system, dict1);
                Assert.IsNotNull(identity);
                Assert.IsNotNull(identity.UserAssignedIdentities);
                Assert.IsTrue(identity.UserAssignedIdentities.Count == 1);
                Assert.IsNotNull(identity.SystemAssignedIdentity);
                Assert.IsTrue(identity.SystemAssignedIdentity.TenantId.Equals(Guid.Empty));
                Assert.IsTrue(identity.SystemAssignedIdentity.PrincipalId.Equals(Guid.Empty));
            } 
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
            dict1["/subscriptions/6b085460-5vbg-477e-ba44-1035046e9101/resourceGroups/tester/providers/Microsoft.Web/sites/autotest"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity = new Identity(system, dict1);
            Identity identity1 = identity;
            Assert.IsTrue(identity.Equals(identity1));
        }

        [TestCase]
        public void EqualsTestTrue()
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9000/resourceGroups/tester/providers/Microsoft.Web/sites/autotest"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity = new Identity(system, dict1);
            var dict2 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict2["/subscriptions/6b085460-5f21-477e-ba44-1035046e9000/resourceGroups/tester/providers/Microsoft.Web/sites/autotest"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system2 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity1 = new Identity(system2, dict2);
            Assert.IsTrue(identity.Equals(identity1));
        }

        [TestCase]
        public void EqualsTestFalse()
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035nbhs9101/resourceGroups/tester/providers/Microsoft.Web/sites/autotest"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity = new Identity(system, dict1);
            var dict2 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict2["/subscriptions/6b08dfsg-5f21-475e-ba44-1035046e9101/resourceGroups/tester/providers/Microsoft.Web/sites/autotest"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system2 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity1 = new Identity(system2, dict2);
            Assert.IsFalse(identity.Equals(identity1));
        }

        [TestCase]
        public void EqualsTestFalseSameKey()
        {
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-10ancd6e9101/resourceGroups/tester/providers/Microsoft.Web/sites/autotest"] = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            var system = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity = new Identity(system, dict1);
            var dict2 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict2["/subscriptions/6b085460-5f21-477e-ba44-10ancd6e9101/resourceGroups/tester/providers/Microsoft.Web/sites/autotest"] = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), Guid.Empty);
            var system2 = new SystemAssignedIdentity(Guid.Empty, Guid.Empty);
            Identity identity1 = new Identity(system2, dict2);
            Assert.IsFalse(identity.Equals(identity1));
        }

        [TestCase]
        public void TestDeserializerInvalidDefaultJson()
        {
            JsonElement invalid = default(JsonElement);
            Assert.Throws<ArgumentException>(delegate { Identity.Deserialize(invalid); });
        }

        public JsonProperty DeserializerHelper(string filename)
        {
            var json = File.ReadAllText("./TestAssets/Identity/" + filename);
            JsonDocument document = JsonDocument.Parse(json);
            JsonElement rootElement = document.RootElement;
            return rootElement.EnumerateObject().First();
        }

        [TestCase]
        public void TestDeserializerInvalidNullType()
        {
            var identityJsonProperty = DeserializerHelper("InvalidTypeIsNull.json");
            Assert.Throws<ArgumentNullException>(delegate { Identity.Deserialize(identityJsonProperty.Value); });
        }

        [TestCase]
        public void TestDeserializerValidSystemAndUserAssigned()
        {
            var identityJsonProperty = DeserializerHelper("SystemAndUserAssignedValid.json");
            Identity back = Identity.Deserialize(identityJsonProperty.Value);
            Assert.IsTrue("22fdaec1-8b9f-49dc-bd72-ddaf8f215577".Equals(back.SystemAssignedIdentity.PrincipalId.ToString()));
            Assert.IsTrue("72f988af-86f1-41af-91ab-2d7cd011db47".Equals(back.SystemAssignedIdentity.TenantId.ToString()));
            var user = back.UserAssignedIdentities;
            Assert.AreEqual("/subscriptions/db1ab6f0-4769-4aa7-930e-01e2ef9c123c/resourceGroups/tester/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity", user.Keys.First().ToString());
            Assert.AreEqual("9a9eaa6a-b49c-4c63-afb5-3b72e3e65422", user.Values.First().ClientId.ToString());
            Assert.AreEqual("77563a98-c9d9-407b-a7af-592d21fa2153", user.Values.First().PrincipalId.ToString());
        }

        [TestCase]
        public void TestDeserializerInvalidType()
        {
            var identityJsonProperty = DeserializerHelper("InvalidType.json");
            Identity back = Identity.Deserialize(identityJsonProperty.Value);
            var user = back.UserAssignedIdentities;
            Assert.AreEqual("/subscriptions/db1ab6f0-4769-tgds-930e-01e2ef9c123c/resourceGroups/tester/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity", user.Keys.First().ToString());
            Assert.AreEqual("9a2eaa6a-b49c-4a63-afb5-3b72e3e65422", user.Values.First().ClientId.ToString());
            Assert.AreEqual("77563a98-c9d9-4f7b-a7af-592d21fa2153", user.Values.First().PrincipalId.ToString());
        }

        [TestCase]
        public void TestDeserializerValidInnerExtraField()
        {
            var identityJsonProperty = DeserializerHelper("SystemAndUserAssignedInnerExtraField.json");
            Identity back = Identity.Deserialize(identityJsonProperty.Value);
            Assert.IsTrue("22fddec1-8b9f-49dc-bd72-ddaf8f215577".Equals(back.SystemAssignedIdentity.PrincipalId.ToString()));
            Assert.IsTrue("72f988bf-86f1-41af-91ab-2d7cd011db47".Equals(back.SystemAssignedIdentity.TenantId.ToString()));
            var user = back.UserAssignedIdentities;
            Assert.AreEqual("/subscriptions/db1ab6f0-4769-4b27-9dde-01e2ef9c123c/resourceGroups/tester/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity", user.Keys.First().ToString());
            Assert.AreEqual("9a9eaa6a-b49c-4c63-afb5-3b72e3e65422", user.Values.First().ClientId.ToString());
            Assert.AreEqual("77563a98-c9d9-407b-a7af-592d21fa2153", user.Values.First().PrincipalId.ToString());
        }

        [TestCase]
        public void TestDeserializerValidMiddleExtraField()
        {
            var identityJsonProperty = DeserializerHelper("SystemAndUserAssignedMiddleExtraField.json");
            Identity back = Identity.Deserialize(identityJsonProperty.Value);
            Assert.IsTrue("22fddec1-8b9f-49dc-bd72-ddaf8f215577".Equals(back.SystemAssignedIdentity.PrincipalId.ToString()));
            Assert.IsTrue("72f988bf-86f1-41af-91ab-2d7cd011db47".Equals(back.SystemAssignedIdentity.TenantId.ToString()));
            var user = back.UserAssignedIdentities;
            Assert.AreEqual("/subscriptions/db1ab6f0-4769-4b27-9dde-01e2ef9c123c/resourceGroups/tester/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity", user.Keys.First().ToString());
            Assert.AreEqual("9a9eaa6a-b49c-4c63-afb5-3b72e3e65422", user.Values.First().ClientId.ToString());
            Assert.AreEqual("77563a98-c9d9-407b-a7af-592d21fa2153", user.Values.First().PrincipalId.ToString());
        }

        [TestCase]
        public void TestDeserializerValidOuterExtraField()
        {
            var json = File.ReadAllText("./TestAssets/Identity/SystemAndUserAssignedOuterExtraField.json");
            JsonDocument document = JsonDocument.Parse(json);
            JsonElement rootElement = document.RootElement;
            var identityJsonProperty = rootElement.EnumerateObject().ElementAt(1);
            Identity back = Identity.Deserialize(identityJsonProperty.Value);
            Assert.IsTrue("22fddec1-8b9f-49dc-bd72-ddaf8f215577".Equals(back.SystemAssignedIdentity.PrincipalId.ToString()));
            Assert.IsTrue("72f988bf-86f1-41af-91ab-2d7cd011db47".Equals(back.SystemAssignedIdentity.TenantId.ToString()));
            var user = back.UserAssignedIdentities;
            Assert.AreEqual("/subscriptions/db1ab6f0-3466-4b27-930e-01e2ef9c123c/resourceGroups/tester/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity", user.Keys.First().ToString());
            Assert.AreEqual("9a2eaa6b-b49c-4c63-afb5-3b72e3e65422", user.Values.First().ClientId.ToString());
            Assert.AreEqual("7756fa98-c9d9-477b-a7af-592d21fa2153", user.Values.First().PrincipalId.ToString());
        }

        [TestCase]
        public void TestDeserializerValidSystemAndMultUser()
        {
            var identityJsonProperty = DeserializerHelper("SystemAndUserAssignedValidMultIdentities.json");
            Identity back = Identity.Deserialize(identityJsonProperty.Value);
            Assert.IsTrue("22fddec1-8b9f-49dc-bd72-ddaf8f215570".Equals(back.SystemAssignedIdentity.PrincipalId.ToString()));
            Assert.IsTrue("72f988bf-86f1-41af-91ab-2d7cd011db40".Equals(back.SystemAssignedIdentity.TenantId.ToString()));
            var user = back.UserAssignedIdentities;
            Assert.AreEqual("/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123z/resourceGroups/tester/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity0", user.Keys.First().ToString());
            Assert.AreEqual("9a2eaa6a-b49c-4c63-afb5-3b72e3e65422", user.Values.First().ClientId.ToString());
            Assert.AreEqual("77563a98-c9d9-477b-a7af-592d21fa2153", user.Values.First().PrincipalId.ToString());
            Assert.AreEqual("/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9cfrgh/resourceGroups/tester/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity1", user.Keys.ElementAt(1).ToString());
            Assert.AreEqual("9a2eaa6a-b49c-4c63-afb5-3b72e3c65420", user.Values.ElementAt(1).ClientId.ToString());
            Assert.AreEqual("77563a98-c9d9-477b-a7af-592d2bfa2150", user.Values.ElementAt(1).PrincipalId.ToString());
        }

        [TestCase]
        public void TestDeserializerValidSystemAssigned()
        {
            var identityJsonProperty = DeserializerHelper("SystemAssigned.json");
            Identity back = Identity.Deserialize(identityJsonProperty.Value);
            Assert.IsTrue("22fddec1-8b9f-49dc-bd72-ddaf8f215577".Equals(back.SystemAssignedIdentity.PrincipalId.ToString()));
            Assert.IsTrue("72f988bf-86f1-41af-91ab-2d7cd011db47".Equals(back.SystemAssignedIdentity.TenantId.ToString()));
            Assert.IsTrue(back.UserAssignedIdentities.Count == 0);            
        }

        [TestCase]
        public void TestDeserializerValidUserAssigned()
        {
            var identityJsonProperty = DeserializerHelper("UserAssigned.json");
            Identity back = Identity.Deserialize(identityJsonProperty.Value);
            Assert.IsNull(back.SystemAssignedIdentity);
            var user = back.UserAssignedIdentities;
            Assert.AreEqual("/subscriptions/db1ab6f0-4769-4b2e-930e-01e2ef9c123c/resourceGroups/tester-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testidentity", user.Keys.First().ToString());
            Assert.AreEqual("9a2eaa6a-b49c-4c63-afb5-3b72e3e65422", user.Values.First().ClientId.ToString());
            Assert.AreEqual("77563a98-c9d9-477b-a7af-592d21fa2153", user.Values.First().PrincipalId.ToString());
        }

        [TestCase]
        public void TestSerializerValidSystemAndUser()
        {
            SystemAssignedIdentity systemAssignedIdentity = new SystemAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = userAssignedIdentity;
            Identity identity = new Identity(systemAssignedIdentity, dict1);
            string system = "\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\",\"tenantId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\"";
            string user = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}";
            string expected = "{\"identity\":{" +
                system + "," +
                "\"kind\":\"SystemAssigned, UserAssigned\"," +
                "\"userAssignedIdentities\":" +
                "{" + "\"/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport\":" +
                user + "}}}";
            string actual = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    actual = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestSerializerValidSystemAndMultUser()
        {
            SystemAssignedIdentity systemAssignedIdentity = new SystemAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            UserAssignedIdentity userAssignedIdentity1 = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            UserAssignedIdentity userAssignedIdentity2 = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011cb47"), new Guid("de29bab1-49e1-4705-819b-4dfddcebaa98"));
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport1"] = userAssignedIdentity1;
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport2"] = userAssignedIdentity2;
            Identity identity = new Identity(systemAssignedIdentity, dict1);
            string system = "\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\",\"tenantId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\"";
            string user = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}";
            string user2 = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011cb47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddcebaa98\"}";
            string expected = "{\"identity\":{" +
                system + "," +
                "\"kind\":\"SystemAssigned, UserAssigned\"," +
                "\"userAssignedIdentities\":" +
                "{" + "\"/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport1\":" +
                user + "," +
                "\"/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport2\":" +
                user2 + "}}}";
            string actual = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    actual = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestSerializerValidSystemOnly()
        {
            SystemAssignedIdentity systemAssignedIdentity = new SystemAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            Identity identity = new Identity(systemAssignedIdentity, null);
            string system = "\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\",\"tenantId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\"";
            string expected = "{\"identity\":{" +
                system + "," +
                "\"kind\":\"SystemAssigned\"}}";
            string actual = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    actual = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestSerializerValidUserEmptySystem()
        {
            UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = userAssignedIdentity;
            Identity identity = new Identity(dict1, true);
            string system = "\"principalId\":\"null\",\"tenantId\":\"null\"";
            string user = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}";
            string expected = "{\"identity\":{" +
                system + "," +
                "\"kind\":\"SystemAssigned, UserAssigned\"," +
                "\"userAssignedIdentities\":" +
                "{" + "\"/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport\":" +
                user + "}}}";
            string actual = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    actual = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestSerializerValidUserNullSystem()
        {
            UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            var dict1 = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            dict1["/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport"] = userAssignedIdentity;
            Identity identity = new Identity(dict1, false);
            string user = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}";
            string expected = "{\"identity\":{" +
                "\"kind\":\"UserAssigned\"," +
                "\"userAssignedIdentities\":" +
                "{" + "\"/subscriptions/6b085460-5f21-477e-ba44-1035046e9101/resourceGroups/nbhatia_test/providers/Microsoft.Web/sites/autoreport\":" +
                user + "}}}";
            string actual = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    actual = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestSerializerValidIdentityNull()
        {
            Identity identity = new Identity();
            string expected = "{\"identity\":\"null\"}";
            string actual = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Identity.Serialize(writer, identity);
                    stream.Seek(0, SeekOrigin.Begin);
                    actual = streamReader.ReadToEnd();
                }
            }
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestSerializerInvalidNullWriter()
        {
            Identity identity = new Identity();
            using (Stream stream = new MemoryStream())
            {
                Assert.Throws<ArgumentNullException>(delegate { Identity.Serialize(null, identity); });
            }
        }

        [TestCase]
        public void TestSerializerInvalidNullIdentity()
        {
            Identity identity = null;
            using (Stream stream = new MemoryStream())
            {
                var writer = new Utf8JsonWriter(stream);
                Assert.Throws<ArgumentNullException>(delegate { Identity.Serialize(writer, identity); });
            }
        }
    }
}
