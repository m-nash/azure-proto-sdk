using azure_proto_core;
using NUnit.Framework;
using System;

namespace azure_proto_core_test
{
    public class IdentityKindTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void CompareToZeroIdentityKind()
        {
            IdentityKind ik1 = IdentityKind.UserAssigned;
            IdentityKind ik2 = IdentityKind.UserAssigned;
            Assert.AreEqual(0, ik1.CompareTo(ik2));

            IdentityKind ik3 = IdentityKind.SystemAssigned;
            IdentityKind ik4 = IdentityKind.SystemAssigned;
            Assert.AreEqual(0, ik3.CompareTo(ik4));

            IdentityKind ik5 = IdentityKind.SystemAndUserAssigned;
            IdentityKind ik6 = IdentityKind.SystemAndUserAssigned;
            Assert.AreEqual(0, ik5.CompareTo(ik6));

            IdentityKind ik7 = new IdentityKind(null);
            IdentityKind ik8 = new IdentityKind(null);
            Assert.AreEqual(0, ik7.CompareTo(ik8));
        }

        [Test]
        public void CompareToOneIdentityKind()
        {
            Assert.AreEqual(1, IdentityKind.SystemAssigned.CompareTo(null));
            Assert.AreEqual(1, IdentityKind.UserAssigned.CompareTo(null));
            Assert.AreEqual(1, IdentityKind.SystemAndUserAssigned.CompareTo(null));

            Assert.AreEqual(1, IdentityKind.UserAssigned.CompareTo(IdentityKind.SystemAssigned));
            Assert.AreEqual(1, IdentityKind.UserAssigned.CompareTo(IdentityKind.SystemAndUserAssigned));

            Assert.AreEqual(1, IdentityKind.SystemAssigned.CompareTo(IdentityKind.SystemAndUserAssigned));
        }

        [Test]
        public void CompareToMinusOneIdentityKind()
        {
            Assert.AreEqual(-1, IdentityKind.SystemAssigned.CompareTo(IdentityKind.UserAssigned));
            Assert.AreEqual(-1, IdentityKind.SystemAndUserAssigned.CompareTo(IdentityKind.UserAssigned));

            IdentityKind nullAssigned = new IdentityKind(null);
            Assert.AreEqual(-1, nullAssigned.CompareTo(IdentityKind.UserAssigned));
            Assert.AreEqual(-1, nullAssigned.CompareTo(IdentityKind.SystemAssigned));
            Assert.AreEqual(-1, nullAssigned.CompareTo(IdentityKind.SystemAndUserAssigned));
        }

        [TestCase]
        public void EqualsMethodTrueIdentityKind()
        {
            IdentityKind ik1 = IdentityKind.UserAssigned;
            IdentityKind ik2 = IdentityKind.UserAssigned;
            Assert.AreEqual(true, ik1.Equals(ik2));

            IdentityKind ik3 = IdentityKind.SystemAssigned;
            IdentityKind ik4 = IdentityKind.SystemAssigned;
            Assert.AreEqual(true, ik3.Equals(ik4));

            IdentityKind ik5 = IdentityKind.SystemAndUserAssigned;
            IdentityKind ik6 = IdentityKind.SystemAndUserAssigned;
            Assert.AreEqual(true, ik5.Equals(ik6));

            IdentityKind ik7 = new IdentityKind(null);
            IdentityKind ik8 = new IdentityKind(null);
            Assert.AreEqual(true, ik7.Equals(ik8));
        }

        [TestCase]
        public void EqualsMethodFalseIdentityKind()
        {
            Assert.AreEqual(false, IdentityKind.SystemAssigned.Equals(null));
            Assert.AreEqual(false, IdentityKind.UserAssigned.Equals(null));
            Assert.AreEqual(false, IdentityKind.SystemAndUserAssigned.Equals(null));

            Assert.AreEqual(false, IdentityKind.UserAssigned.Equals(IdentityKind.SystemAssigned));
            Assert.AreEqual(false, IdentityKind.UserAssigned.Equals(IdentityKind.SystemAndUserAssigned));

            IdentityKind nullAssigned = new IdentityKind(null);
            Assert.AreEqual(false, nullAssigned.Equals(IdentityKind.SystemAssigned));
            Assert.AreEqual(false, nullAssigned.Equals(IdentityKind.UserAssigned));
            Assert.AreEqual(false, nullAssigned.Equals(IdentityKind.SystemAndUserAssigned));
        }

        [Test]
        public void CompareToZeroString()
        {
            IdentityKind ik1 = new IdentityKind("UserAssigned");
            IdentityKind ik2 = new IdentityKind("UserAssigned");
            Assert.AreEqual(0, ik1.CompareTo(ik2.Value));

            IdentityKind ik3 = new IdentityKind("SystemAssigned");
            IdentityKind ik4 = new IdentityKind("SystemAssigned");
            Assert.AreEqual(0, ik3.CompareTo(ik4.Value));

            IdentityKind ik5 = new IdentityKind("SystemAndUserAssigned");
            IdentityKind ik6 = new IdentityKind("SystemAndUserAssigned");
            Assert.AreEqual(0, ik5.CompareTo(ik6.Value));

            IdentityKind ik7 = new IdentityKind(null);
            IdentityKind ik8 = new IdentityKind(null);
            Assert.AreEqual(0, ik7.CompareTo(ik8.Value));
        }

        [Test]
        public void CompareToOneString()
        {
            IdentityKind systemAssigned = new IdentityKind("SystemAssigned");
            IdentityKind userAssigned = new IdentityKind("UserAssigned");
            IdentityKind systemAndUserAssigned = new IdentityKind("SystemAndUserAssigned");
            Assert.AreEqual(1, systemAssigned.CompareTo(null));
            Assert.AreEqual(1, userAssigned.CompareTo(null));
            Assert.AreEqual(1, systemAndUserAssigned.CompareTo(null));

            Assert.AreEqual(1, userAssigned.CompareTo(systemAssigned.Value));
            Assert.AreEqual(1, userAssigned.CompareTo(systemAndUserAssigned.Value));

            Assert.AreEqual(1, systemAssigned.CompareTo(systemAndUserAssigned.Value));
        }

        [Test]
        public void CompareToMinusOneString()
        {
            IdentityKind systemAssigned = new IdentityKind("SystemAssigned");
            IdentityKind userAssigned = new IdentityKind("UserAssigned");
            IdentityKind systemAndUserAssigned = new IdentityKind("SystemAndUserAssigned");
            IdentityKind nullAssigned = new IdentityKind(null);

            Assert.AreEqual(-1, systemAssigned.CompareTo(userAssigned.Value));
            Assert.AreEqual(-1, systemAndUserAssigned.CompareTo(userAssigned.Value));

            Assert.AreEqual(-1, nullAssigned.CompareTo(systemAssigned.Value));
            Assert.AreEqual(-1, nullAssigned.CompareTo(userAssigned.Value));
            Assert.AreEqual(-1, nullAssigned.CompareTo(systemAndUserAssigned.Value));
        }

        [TestCase]
        public void EqualsMethodTrueString()
        {
            IdentityKind ik1 = new IdentityKind("UserAssigned");
            IdentityKind ik2 = new IdentityKind("UserAssigned");
            Assert.AreEqual(true, ik1.Equals(ik2.Value));

            IdentityKind ik3 = new IdentityKind("SystemAssigned");
            IdentityKind ik4 = new IdentityKind("SystemAssigned");
            Assert.AreEqual(true, ik3.Equals(ik4.Value));

            IdentityKind ik5 = new IdentityKind("SystemAndUserAssigned");
            IdentityKind ik6 = new IdentityKind("SystemAndUserAssigned");
            Assert.AreEqual(true, ik5.Equals(ik6.Value));

            IdentityKind ik7 = new IdentityKind(null);
            IdentityKind ik8 = new IdentityKind(null);
            Assert.AreEqual(true, ik7.Equals(ik8.Value));
        }

        [TestCase]
        public void EqualsMethodFalseString()
        {
            IdentityKind systemAssigned = new IdentityKind("SystemAssigned");
            IdentityKind userAssigned = new IdentityKind("UserAssigned");
            IdentityKind systemAndUserAssigned = new IdentityKind("SystemAndUserAssigned");
            IdentityKind nullAssigned = new IdentityKind(null);

            Assert.AreEqual(false, systemAssigned.Equals(null));
            Assert.AreEqual(false, userAssigned.Equals(null));
            Assert.AreEqual(false, systemAndUserAssigned.Equals(null));

            Assert.AreEqual(false, userAssigned.Equals(systemAssigned.Value));
            Assert.AreEqual(false, userAssigned.Equals(systemAndUserAssigned.Value));

            Assert.AreEqual(false, nullAssigned.Equals(systemAssigned.Value));
            Assert.AreEqual(false, nullAssigned.Equals(userAssigned.Value));
            Assert.AreEqual(false, nullAssigned.Equals(systemAndUserAssigned.Value));
        }
    }
}
