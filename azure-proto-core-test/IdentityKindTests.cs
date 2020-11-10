using azure_proto_core;
using NUnit.Framework;

namespace azure_proto_core_test
{
    public class IdentityKindTests
    {
        
        [TestCase(null, null)]
        [TestCase("UserAssigned", "UserAssigned")]
        [TestCase("SystemAssigned", "SystemAssigned")]
        [TestCase("SystemAndUserAssigned", "SystemAndUserAssigned")]
        [TestCase("MyIdentity", "MyIdentity")]        
        public void CompareToZeroIdentityKind(string kind1, string kind2)
        {
            IdentityKind ik1 = new IdentityKind(kind1);
            IdentityKind ik2 = new IdentityKind(kind2);
            Assert.AreEqual(0, ik1.CompareTo(ik2));
        }

        [TestCase("UserAssigned", null)]
        [TestCase("SystemAssigned", null)]
        [TestCase("SystemAndUserAssigned", null)]
        [TestCase("UserAssigned", "SystemAssigned")]
        [TestCase("UserAssigned", "SystemAndUserAssigned")]
        [TestCase("SystemAssigned", "SystemAndUserAssigned")]
        [TestCase("SystemAssigned", "MyIdentity")]
        public void CompareToOneIdentityKind(string kind1, string kind2)
        {
            IdentityKind ik1 = new IdentityKind(kind1);
            IdentityKind ik2 = new IdentityKind(kind2);
            Assert.AreEqual(1, ik1.CompareTo(ik2));
        }

        [TestCase("SystemAssigned", "UserAssigned")]
        [TestCase("SystemAndUserAssigned", "UserAssigned")]
        [TestCase(null, "UserAssigned")]
        [TestCase(null, "SystemAssigned")]
        [TestCase(null, "SystemAndUserAssigned")]
        [TestCase(null, "MyIdentity")]
        public void CompareToMinusOneIdentityKind(string kind1, string kind2)
        {
            IdentityKind ik1 = new IdentityKind(kind1);
            IdentityKind ik2 = new IdentityKind(kind2);
            Assert.AreEqual(-1, ik1.CompareTo(ik2));
        }

        [TestCase(null, null)]
        [TestCase("UserAssigned", "UserAssigned")]
        [TestCase("SystemAssigned", "SystemAssigned")]
        [TestCase("SystemAndUserAssigned", "SystemAndUserAssigned")]       
        [TestCase("MyIdentity", "MyIdentity")]       
        public void EqualsMethodTrueIdentityKind(string kind1, string kind2)
        {
            IdentityKind ik1 = new IdentityKind(kind1);
            IdentityKind ik2 = new IdentityKind(kind2);
            Assert.AreEqual(true, ik1.Equals(ik2));
        }

        [TestCase(null, "UserAssigned")]
        [TestCase(null, "SystemAssigned")]
        [TestCase(null, "SystemAndUserAssigned")]
        [TestCase("UserAssigned", "SystemAssigned")]
        [TestCase("UserAssigned", "SystemAndUserAssigned")]
        [TestCase("UserAssigned", null)]
        [TestCase("SystemAssigned", null)]
        [TestCase("SystemAndUserAssigned", null)]
        [TestCase("SystemAndUserAssigned", "MyIdentity")]
        public void EqualsMethodFalseIdentityKind(string kind1, string kind2)
        {
            IdentityKind ik1 = new IdentityKind(kind1);
            IdentityKind ik2 = new IdentityKind(kind2);
            Assert.AreEqual(false, ik1.Equals(ik2));
        }

        [TestCase("SystemAssigned", "SystemAssigned")]
        [TestCase("UserAssigned", "UserAssigned")]
        [TestCase("SystemAndUserAssigned", "SystemAndUserAssigned")]
        [TestCase("MyIdentity", "MyIdentity")]
        [TestCase(null, null)]
        public void CompareToZeroString(string kind1, string kind2)
        {
            IdentityKind ik1 = new IdentityKind(kind1);
            Assert.AreEqual(0, ik1.CompareTo(kind2));
        }

        [TestCase("SystemAssigned", null)]
        [TestCase("UserAssigned", null)]
        [TestCase("SystemAndUserAssigned", null)]
        [TestCase("UserAssigned", "SystemAssigned")]
        [TestCase("UserAssigned", "SystemAndUserAssigned")]
        [TestCase("SystemAssigned", "SystemAndUserAssigned")]
        [TestCase("UserAssigned", "MyIdentity")]
        public void CompareToOneString(string kind1, string kind2)
        {
            IdentityKind ik1 = new IdentityKind(kind1);
            Assert.AreEqual(1, ik1.CompareTo(kind2));
        }

        [TestCase("SystemAssigned", "UserAssigned")]
        [TestCase("SystemAndUserAssigned", "UserAssigned")]
        [TestCase(null, "UserAssigned")]
        [TestCase(null, "SystemAssigned")]
        [TestCase(null, "SystemAndUserAssigned")]
        [TestCase(null, "MyIdentity")]
        public void CompareToMinusOneString(string kind1, string kind2)
        {
            IdentityKind ik1 = new IdentityKind(kind1);            
            Assert.AreEqual(-1, ik1.CompareTo(kind2));
        }

        [TestCase("UserAssigned", "UserAssigned")]
        [TestCase("SystemAssigned", "SystemAssigned")]
        [TestCase("SystemAndUserAssigned", "SystemAndUserAssigned")]
        [TestCase("MyIdentity", "MyIdentity")]
        [TestCase(null, null)]
        public void EqualsMethodTrueString(string kind1, string kind2)
        {
            IdentityKind ik1 = new IdentityKind(kind1);
            Assert.AreEqual(true, ik1.Equals(kind2));
        }

        [TestCase ("SystemAssigned", null)]
        [TestCase ("UserAssigned", null)]
        [TestCase ("SystemAndUserAssigned", null)]
        [TestCase("UserAssigned", "SystemAssigned")]
        [TestCase("UserAssigned", "SystemAndUserAssigned")]
        [TestCase("UserAssigned", "MyIdentity")]
        [TestCase(null, "SystemAssigned")]
        [TestCase(null, "UserAssigned")]
        [TestCase(null, "SystemAndUserAssigned")]
        public void EqualsMethodFalseString(string kind1, string kind2)
        {
            IdentityKind identityKind = new IdentityKind(kind1);
            Assert.AreEqual(false, identityKind.Equals(kind2));
        }

        [Test]
        public void CheckStaticVars()
        {
            Assert.AreEqual(true, IdentityKind.UserAssigned.Value.Equals("UserAssigned"));
            Assert.AreEqual(true, IdentityKind.SystemAssigned.Value.Equals("SystemAssigned"));
            Assert.AreEqual(true, IdentityKind.SystemAndUserAssigned.Value.Equals("SystemAndUserAssigned"));
        }
    }
}
