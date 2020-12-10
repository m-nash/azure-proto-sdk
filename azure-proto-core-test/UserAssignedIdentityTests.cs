using azure_proto_core.Resources;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace azure_proto_core_test
{
    public class UserAssignedIdentityTests : UserAssignedIdentity
    {

        [TestCase(null, null, null, null, true)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", false)]
        public void CompareToZero(string clientId1, string principalId1, string clientId2, string principalId2, bool isNull)
        {
            UserAssignedIdentity identity1;
            UserAssignedIdentity identity2;
            if (isNull)
            {
                identity1 = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
                identity2 = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            }

            else
            {
                identity1 = new UserAssignedIdentity(new Guid(clientId1), new Guid(principalId1));
                identity2 = new UserAssignedIdentity(new Guid(clientId2), new Guid(principalId2));
            }

            Assert.AreEqual(0, identity1.CompareTo(identity2));
        }

        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", null, null, true)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db46", "de29bab1-49e1-4705-819b-4dfddceaaa97", false)]
        public void CompareToOne(string clientId1, string principalId1, string clientId2, string principalId2, bool isNull)
        {
            UserAssignedIdentity identity1 = new UserAssignedIdentity(new Guid(clientId1), new Guid(principalId1));
            UserAssignedIdentity identity2;
            if (isNull)
            {
                identity2 = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            }

            else
            {
                identity2 = new UserAssignedIdentity(new Guid(clientId2), new Guid(principalId2));
            }

            Assert.AreEqual(1, identity1.CompareTo(identity2));
        }

        [TestCase(null, null, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", true)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db48", "de29bab1-49e1-4705-819b-4dfddceaaa99", false)]
        public void CompareToMinusOne(string clientId1, string principalId1, string clientId2, string principalId2, bool isNull)
        {
            UserAssignedIdentity identity1;
            UserAssignedIdentity identity2 = new UserAssignedIdentity(new Guid(clientId2), new Guid(principalId2));
            if (isNull)
            {
                identity1 = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            }

            else
            {
                identity1 = new UserAssignedIdentity(new Guid(clientId1), new Guid(principalId1));
            }

            Assert.AreEqual(-1, identity1.CompareTo(identity2));
        }

        [TestCase(null, null, null, null, true)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", false)]
        public void EqualsMethodTrue(string clientId1, string principalId1, string clientId2, string principalId2, bool isNull)
        {
            UserAssignedIdentity identity1;
            UserAssignedIdentity identity2;
            if (isNull)
            {
                identity1 = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
                identity2 = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            }

            else
            {
                identity1 = new UserAssignedIdentity(new Guid(clientId1), new Guid(principalId1));
                identity2 = new UserAssignedIdentity(new Guid(clientId2), new Guid(principalId2));
            }

            Assert.IsTrue(identity1.Equals(identity2));
        }

        [TestCase(null, null, "72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", true, false)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", "72f988bf-86f1-41af-91ab-2d7cd011db44", "de29bab1-49e1-4705-819b-4dfddceaaa94", false, false)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa98", null, null, false, true)]
        [TestCase("72f988bf-86f1-41af-91ab-2d7cd011db47", "de29bab1-49e1-4705-819b-4dfddceaaa93", "72f988bf-86f1-41af-91ab-2d7cd011db42", "de29bab1-49e1-4705-819b-4dfddceaaa91", false, false)]
        public void EqualsMethodFalse(string clientId1, string principalId1, string clientId2, string principalId2, bool isNull1, bool isNull2)
        {
            UserAssignedIdentity identity1;
            UserAssignedIdentity identity2;
            if (isNull1)
            {
                identity1 = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
                identity2 = new UserAssignedIdentity(new Guid(clientId2), new Guid(principalId2));
            }
            else if (isNull2)
            {
                identity1 = new UserAssignedIdentity(new Guid(clientId1), new Guid(principalId1));
                identity2 = new UserAssignedIdentity(Guid.Empty, Guid.Empty);
            }
            else
            {
                identity1 = new UserAssignedIdentity(new Guid(clientId1), new Guid(principalId1));
                identity2 = new UserAssignedIdentity(new Guid(clientId2), new Guid(principalId2));
            }

            Assert.IsFalse(identity1.Equals(identity2));
        }

        [TestCase]
        public void TestDeserializerValid()
        {
            string json = "";
            string path = ((AssemblyMetadataAttribute)GetType().Assembly.GetCustomAttribute(typeof(AssemblyMetadataAttribute))).Value;
            using (StreamReader f = new StreamReader(path + "/TestAssets/UserAssignedIdentity/UserAssignedValid.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            var properties = document.RootElement.EnumerateObject().First().Value;
            UserAssignedIdentity back = new UserAssignedIdentity();
            foreach (var property in properties.EnumerateObject())
            {
                if (property.NameEquals("userAssignedIdentities"))
                {
                    foreach (var keyValuePair in property.Value.EnumerateObject())
                    {
                        back = Deserialize(keyValuePair.Value);
                    }
                }
            }
            Assert.IsTrue("3beb288c-c3f9-4300-896f-02fbf175b6be".Equals(back.ClientId.ToString()));
            Assert.IsTrue("d0416856-d6cf-466d-8d64-ddc8d7782096".Equals(back.PrincipalId.ToString()));
        }

        [TestCase]
        public void TestDeserializerValidExtraField()
        {
            string json = "";
            string path = ((AssemblyMetadataAttribute)GetType().Assembly.GetCustomAttribute(typeof(AssemblyMetadataAttribute))).Value;
            using (StreamReader f = new StreamReader(path + "/TestAssets/UserAssignedIdentity/UserAssignedExtraField.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            var properties = document.RootElement.EnumerateObject().First().Value;
            UserAssignedIdentity back = new UserAssignedIdentity();
            foreach (var property in properties.EnumerateObject())
            {
                if (property.NameEquals("userAssignedIdentities"))
                {
                    foreach (var keyValuePair in property.Value.EnumerateObject())
                    {
                        back = Deserialize(keyValuePair.Value);
                    }
                }
            }
            Assert.IsTrue("3beb288c-c3f9-4300-896f-02fbf175b6be".Equals(back.ClientId.ToString()));
            Assert.IsTrue("d0416856-d6cf-466d-8d64-ddc8d7782096".Equals(back.PrincipalId.ToString()));
        }

        [TestCase]
        public void TestDeserializerBothValuesNull()
        {
            string json = "";
            string path = ((AssemblyMetadataAttribute)GetType().Assembly.GetCustomAttribute(typeof(AssemblyMetadataAttribute))).Value;
            using (StreamReader f = new StreamReader(path + "/TestAssets/UserAssignedIdentity/UserAssignedBothValuesNull.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            var properties = document.RootElement.EnumerateObject().First().Value;
            UserAssignedIdentity back = new UserAssignedIdentity();
            foreach (var property in properties.EnumerateObject())
            {
                if (property.NameEquals("userAssignedIdentities"))
                {
                    foreach (var keyValuePair in property.Value.EnumerateObject())
                    {
                        back = Deserialize(keyValuePair.Value);
                    }
                }
            }
            Assert.IsNull(back);
        }

        [TestCase]
        public void TestDeserializerBothEmptyString()
        {
            string json = "";
            string path = ((AssemblyMetadataAttribute)GetType().Assembly.GetCustomAttribute(typeof(AssemblyMetadataAttribute))).Value;
            using (StreamReader f = new StreamReader(path + "/TestAssets/UserAssignedIdentity/UserAssignedBothEmptyString.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            var properties = document.RootElement.EnumerateObject().First().Value;
            foreach (var property in properties.EnumerateObject())
            {
                if (property.NameEquals("userAssignedIdentities"))
                {
                    foreach (var keyValuePair in property.Value.EnumerateObject())
                    {
                        Assert.Throws<FormatException>(delegate { Deserialize(keyValuePair.Value); });
                    }
                }
            }
        }

        [TestCase]
        public void TestDeserializerOneEmptyString()
        {
            string json = "";
            string path = ((AssemblyMetadataAttribute)GetType().Assembly.GetCustomAttribute(typeof(AssemblyMetadataAttribute))).Value;
            using (StreamReader f = new StreamReader(path + "/TestAssets/UserAssignedIdentity/UserAssignedOneEmptyString.json"))
                json = f.ReadToEnd();
            JsonDocument document = JsonDocument.Parse(json);
            var properties = document.RootElement.EnumerateObject().First().Value;
            foreach (var property in properties.EnumerateObject())
            {
                if (property.NameEquals("userAssignedIdentities"))
                {
                    foreach (var keyValuePair in property.Value.EnumerateObject())
                    {
                        Assert.Throws<FormatException>(delegate { Deserialize(keyValuePair.Value); });
                    }
                }
            }
        }

        [TestCase]
        public void TestDeserializerOneValueNull()
        {
            string json = "";
            string path = ((AssemblyMetadataAttribute)GetType().Assembly.GetCustomAttribute(typeof(AssemblyMetadataAttribute))).Value;
            using (StreamReader f = new StreamReader(path + "/TestAssets/UserAssignedIdentity/UserAssignedOneValueNull.json"))
                json = f.ReadToEnd();
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
        public void TestDeserializerInvalid()
        {
            string json = "";
            string path = ((AssemblyMetadataAttribute)GetType().Assembly.GetCustomAttribute(typeof(AssemblyMetadataAttribute))).Value;
            using (StreamReader f = new StreamReader(path + "/TestAssets/UserAssignedIdentity/UserAssignedInvalid.json"))
                json = f.ReadToEnd();
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
        public void TestDeserializerInvalidMultipleIdentities()
        {
            string json = "";
            string path = ((AssemblyMetadataAttribute)GetType().Assembly.GetCustomAttribute(typeof(AssemblyMetadataAttribute))).Value;
            using (StreamReader f = new StreamReader(path + "/TestAssets/UserAssignedIdentity/UserAssignedInvalidMultipleIdentities.json"))
                json = f.ReadToEnd();
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
        public void TestSerializer()
        {
            UserAssignedIdentity UserAssignedIdentity = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            string value = "";
            using (Stream stream = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    var writer = new Utf8JsonWriter(stream);
                    Serialize(writer, UserAssignedIdentity);
                    stream.Seek(0, SeekOrigin.Begin);
                    value = streamReader.ReadToEnd();
                }
            }
            string actual = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}";
            Assert.AreEqual(actual, value);
        }

        [TestCase]
        public void TestDeserializerDict()
        {
            string json = "";
            string path = ((AssemblyMetadataAttribute)GetType().Assembly.GetCustomAttribute(typeof(AssemblyMetadataAttribute))).Value;
            using (StreamReader f = new StreamReader(path + "/TestAssets/UserAssignedIdentity/UserAssignedValidMultipleIdentities.json"))
                json = f.ReadToEnd();
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

        public void TestSerializerArray()
        {
            UserAssignedIdentity userAssignedIdentity1 = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db47"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa98"));
            UserAssignedIdentity userAssignedIdentity2 = new UserAssignedIdentity(new Guid("72f988bf-86f1-41af-91ab-2d7cd011db49"), new Guid("de29bab1-49e1-4705-819b-4dfddceaaa90"));
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
            string actual = "{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db47\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa98\"}{\"clientId\":\"72f988bf-86f1-41af-91ab-2d7cd011db49\",\"principalId\":\"de29bab1-49e1-4705-819b-4dfddceaaa908\"}";
            Assert.AreEqual(actual, value);
        }
    }
}
