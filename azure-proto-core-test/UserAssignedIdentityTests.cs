using azure_proto_core.Resources;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace azure_proto_core_test
{
    public class UserAssignedIdentityTests : UserAssignedIdentity
    {
        [TestCase(0, null, null, null, null)]
        [TestCase(0, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98")]
        [TestCase(1, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", null, null)]
        [TestCase(1, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db46", "de29bab1-49e1-4705-819b-4dfddceaaa97")]
        [TestCase(-1, null, null, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98")]
        [TestCase(-1, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db48", "de29bab1-49e1-4705-819b-4dfddceaaa99")]
        public void CompareTo(int answer, string clientId1, string principalId1, string clientId2, string principalId2)
        {
            UserAssignedIdentity identity1;
            UserAssignedIdentity identity2;
            if (clientId1 == null)
                identity1 = new UserAssignedIdentity();

            else
                identity1 = new UserAssignedIdentity(new Guid(clientId1), new Guid(principalId1));

            if (clientId2 == null)
                identity2 = new UserAssignedIdentity();

            else
                identity2 = new UserAssignedIdentity(new Guid(clientId2), new Guid(principalId2));

            Assert.AreEqual(answer, identity1.CompareTo(identity2));
        }

        [TestCase(null, null, null, null)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98")]
        public void EqualsMethodTrue(string clientId1, string principalId1, string clientId2, string principalId2)
        {
            UserAssignedIdentity identity1;
            UserAssignedIdentity identity2;
            if (clientId1 == null)
                identity1 = new UserAssignedIdentity();

            else
                identity1 = new UserAssignedIdentity(new Guid(clientId1), new Guid(principalId1));

            if (clientId2 == null)
                identity2 = new UserAssignedIdentity();

            else
                identity2 = new UserAssignedIdentity(new Guid(clientId2), new Guid(principalId2));

            Assert.IsTrue(identity1.Equals(identity2));
        }

        [TestCase(null, null, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98")]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db44", "de29bab1-49e1-4705-819b-4dfddceaaa94")]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", null, null)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa93", "72f988bf-86f1-41af-91ab-2d7cd011db42", "de29bab1-49e1-4705-819b-4dfddceaaa91")]
        public void EqualsMethodFalse(string clientId1, string principalId1, string clientId2, string principalId2)
        {
            UserAssignedIdentity identity1;
            UserAssignedIdentity identity2;
            if (clientId1 == null)
                identity1 = new UserAssignedIdentity();

            else
                identity1 = new UserAssignedIdentity(new Guid(clientId1), new Guid(principalId1));

            if (clientId2 == null)
                identity2 = new UserAssignedIdentity();

            else
                identity2 = new UserAssignedIdentity(new Guid(clientId2), new Guid(principalId2));

            Assert.IsFalse(identity1.Equals(identity2));
        }

        [TestCase]
        public void EqualsMethodBothIdentitiesEmpty()
        {
            UserAssignedIdentity identity1 = new UserAssignedIdentity();
            UserAssignedIdentity identity2 = new UserAssignedIdentity();
            Assert.IsTrue(identity1.Equals(identity2));
        }

        [TestCase]
        public void EqualsMethodOneIdentityNull()
        {
            UserAssignedIdentity identity1 = new UserAssignedIdentity();
            UserAssignedIdentity identity2 = null;
            Assert.IsFalse(identity1.Equals(identity2));
        }

        [TestCase]
        public void CompareToMethodBothIdentitiesEmpty()
        {
            UserAssignedIdentity identity1 = new UserAssignedIdentity();
            UserAssignedIdentity identity2 = new UserAssignedIdentity();
            Assert.AreEqual(0, identity1.CompareTo(identity2));
        }

        [TestCase]
        public void CompareToMethodOneIdentityNull()
        {
            UserAssignedIdentity identity1 = new UserAssignedIdentity();
            UserAssignedIdentity identity2 = null;
            Assert.AreEqual(1, identity1.CompareTo(identity2));
        }

        public JsonElement DeserializerHelper(string filename)
        {
            var json = File.ReadAllText("./TestAssets/UserAssignedIdentity/" + filename);
            JsonDocument document = JsonDocument.Parse(json);
            JsonElement rootElement = document.RootElement;
            var properties =  rootElement.EnumerateObject().First().Value;
            foreach (var property in properties.EnumerateObject())
            {
                if (property.NameEquals("userAssignedIdentities"))
                {
                    foreach (var keyValuePair in property.Value.EnumerateObject())
                    {
                        return keyValuePair.Value;
                    }
                }
            }
            return default(JsonElement);
        }

        [TestCase]
        public void TestDeserializerDefaultJson()
        {
            JsonElement invalid = default(JsonElement);
            Assert.Throws<ArgumentNullException>(delegate { Deserialize(invalid); });
        }

        [TestCase]
        public void TestDeserializerValid()
        {
            var identityJsonProperty = DeserializerHelper("UserAssignedValid.json");
            UserAssignedIdentity back = Deserialize(identityJsonProperty);
            Assert.IsTrue("3beb288c-c3f9-4300-896f-02fbf175b6be".Equals(back.ClientId.ToString()));
            Assert.IsTrue("d0416856-d6cf-466d-8d64-ddc8d7782096".Equals(back.PrincipalId.ToString()));
        }

        [TestCase]
        public void TestDeserializerValidExtraField()
        {
            var identityJsonProperty = DeserializerHelper("UserAssignedExtraField.json");
            UserAssignedIdentity back = Deserialize(identityJsonProperty);
            Assert.IsTrue("3beb288c-c3f9-4300-896f-02fbf175b6be".Equals(back.ClientId.ToString()));
            Assert.IsTrue("d0416856-d6cf-466d-8d64-ddc8d7782096".Equals(back.PrincipalId.ToString()));
        }

        [TestCase]
        public void TestDeserializerBothValuesNull()
        {
            var identityJsonProperty = DeserializerHelper("UserAssignedBothValuesNull.json");
            var back = Deserialize(identityJsonProperty);
            Assert.IsNull(back);
        }

        [TestCase]
        public void TestDeserializerBothEmptyString()
        {
            var identityJsonProperty = DeserializerHelper("UserAssignedBothEmptyString.json");
            Assert.Throws<FormatException>(delegate { Deserialize(identityJsonProperty); });
        }

        [TestCase]
        public void TestDeserializerOneEmptyString()
        {
            var identityJsonProperty = DeserializerHelper("UserAssignedOneEmptyString.json");
            Assert.Throws<FormatException>(delegate { Deserialize(identityJsonProperty); });
        }

        [TestCase]
        public void TestDeserializerClientIdValueNull()
        {
            var identityJsonProperty = DeserializerHelper("UserAssignedOneValueNull.json");
            Assert.Throws<InvalidOperationException>(delegate { Deserialize(identityJsonProperty); });
        }

        [TestCase]
        public void TestDeserializerPrincipalIdValueNull()
        {
            var identityJsonProperty = DeserializerHelper("UserAssignedOneOtherValueNull.json");
            Assert.Throws<InvalidOperationException>(delegate { Deserialize(identityJsonProperty); });
        }

        [TestCase]
        public void TestDeserializerClientIdInvalid()
        {
            var identityJsonProperty = DeserializerHelper("UserAssignedInvalid.json");
            Assert.Throws<InvalidOperationException>(delegate { Deserialize(identityJsonProperty); });
        }

        [TestCase]
        public void TestDeserializerInvalidMultipleIdentities()
        {
            var json = File.ReadAllText("./TestAssets/UserAssignedIdentity/UserAssignedInvalidMultipleIdentities.json");
            JsonDocument document = JsonDocument.Parse(json);
            var properties = document.RootElement.EnumerateObject().First().Value;
            foreach (var property in properties.EnumerateObject())
            {
                if (property.NameEquals("userAssignedIdentities"))
                {
                    foreach (var keyValuePair in property.Value.EnumerateObject())
                    {
                        Assert.Throws<InvalidOperationException>(delegate { Deserialize(keyValuePair.Value); });
                    }
                }
            }
        }

        [TestCase]
        public void TestDeserializerValidMultipleIdentities()
        {
            var json = File.ReadAllText("./TestAssets/UserAssignedIdentity/UserAssignedValidMultipleIdentities.json");
            JsonDocument document = JsonDocument.Parse(json);
            UserAssignedIdentity[] identities = new UserAssignedIdentity[2];
            var properties = document.RootElement.EnumerateObject().First().Value;
            int count = 0;
            foreach (var property in properties.EnumerateObject())
            {
                if (property.NameEquals("userAssignedIdentities"))
                {
                    foreach (var keyValuePair in property.Value.EnumerateObject())
                    {
                        identities[count] = Deserialize(keyValuePair.Value);
                        count++;
                    }
                }
            }
            Assert.IsTrue("3beb288c-c3f9-4300-896f-02fbf175b6be".Equals(identities[0].ClientId.ToString()));
            Assert.IsTrue("d0416856-d6cf-466d-8d64-ddc8d7782096".Equals(identities[0].PrincipalId.ToString()));
            Assert.IsTrue("fbb39377-ff46-4a82-8c47-42d573180482".Equals(identities[1].ClientId.ToString()));
            Assert.IsTrue("6d63ce43-c3ac-4b03-933d-4bc31eae50b2".Equals(identities[1].PrincipalId.ToString()));
        }

        [TestCase]
        public void TestSerializer()
        {
            UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            string value = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Serialize(writer, userAssignedIdentity);
                    stream.Seek(0, SeekOrigin.Begin);
                    value = streamReader.ReadToEnd();
                }
            }
            string actual = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}";
            Assert.AreEqual(actual, value);
        }

        [TestCase]
        public void TestSerializerEmptyIdentity()
        {
            UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity();
            string value = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Serialize(writer, userAssignedIdentity);
                    stream.Seek(0, SeekOrigin.Begin);
                    value = streamReader.ReadToEnd();
                }
            }
            string actual = "{\"clientId\":\"null\",\"principalId\":\"null\"}";
            Assert.AreEqual(actual, value);
        }

        [TestCase]
        public void TestSerializerNullIdentity()
        {
            UserAssignedIdentity userAssignedIdentity = null;
            using (Stream stream = new MemoryStream())
            {
                var writer = new Utf8JsonWriter(stream);
                Assert.Throws<ArgumentNullException>(delegate { Serialize(writer, userAssignedIdentity); });
            }
        }

        [TestCase]
        public void TestSerializerNullWriter()
        {
            UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity();
            using (Stream stream = new MemoryStream())
            {
                Assert.Throws<ArgumentNullException>(delegate { Serialize(null, userAssignedIdentity); });
            }
        }

        [TestCase]
        public void TestSerializerArray()
        {
            UserAssignedIdentity userAssignedIdentity1 = new UserAssignedIdentity(new Guid("3beb288c-c3f9-4300-896f-02fbf175b6be"), new Guid("d0416856-d6cf-466d-8d64-ddc8d7782096"));
            UserAssignedIdentity userAssignedIdentity2 = new UserAssignedIdentity(new Guid("fbb39377-ff46-4a82-8c47-42d573180482"), new Guid("6d63ce43-c3ac-4b03-933d-4bc31eae50b2"));
            string value = "";
            UserAssignedIdentity[] identities = { userAssignedIdentity1, userAssignedIdentity2 };
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    foreach (var identity in identities)
                    {
                        var writer = new Utf8JsonWriter(stream);
                        Serialize(writer, identity);
                        stream.Seek(0, SeekOrigin.Begin);
                        value = streamReader.ReadToEnd();
                    }
                }
            }
            string actual = "{\"clientId\":\"3beb288c-c3f9-4300-896f-02fbf175b6be\"," +
                "\"principalId\":\"d0416856-d6cf-466d-8d64-ddc8d7782096\"}" +
                "{\"clientId\":\"fbb39377-ff46-4a82-8c47-42d573180482\"," +
                "\"principalId\":\"6d63ce43-c3ac-4b03-933d-4bc31eae50b2\"}";
            Assert.AreEqual(actual, value);
        }

        [TestCase]
        public void TestEqualsBothIdentitiesNull()
        {
            UserAssignedIdentity identity1 = null;
            UserAssignedIdentity identity2 = null;
            Assert.IsTrue(Equals(identity1, identity2));
        }

        [TestCase]
        public void TestEqualsOneIdentityNull()
        {
            UserAssignedIdentity identity1 = null;
            UserAssignedIdentity identity2 = new UserAssignedIdentity();
            Assert.IsFalse(Equals(identity1, identity2));
        }

        [TestCase]
        public void TestEqualsOtherIdentityNull()
        {
            UserAssignedIdentity identity1 = new UserAssignedIdentity();
            UserAssignedIdentity identity2 = null;
            Assert.IsFalse(Equals(identity1, identity2));
        }

        [TestCase]
        public void TestEqualsReference()
        {
            UserAssignedIdentity identity1 = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            UserAssignedIdentity identity2 = identity1;
            Assert.IsTrue(Equals(identity1, identity2));
        }

        [TestCase]
        public void TestEqualsTrue()
        {
            UserAssignedIdentity identity1 = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            UserAssignedIdentity identity2 = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            Assert.IsTrue(Equals(identity1, identity2));
        }

        [TestCase]
        public void TestEqualsFalse()
        {
            UserAssignedIdentity identity1 = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            UserAssignedIdentity identity2 = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db42"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            Assert.IsFalse(Equals(identity1, identity2));
        }
    }
}
