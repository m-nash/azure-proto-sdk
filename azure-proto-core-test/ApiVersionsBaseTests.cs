using azure_proto_core;
using NUnit.Framework;
using System;

namespace azure_proto_core_test
{
    public class ApiVersionsBaseTests
    {
        [TestCase]
        public void VersionToString()
        {
            ArmClientOptions options = new ArmClientOptions();
            Assert.AreEqual("2020-06-01", options.FakeRpRestVersions().FakeResourceVersion);
        }

        [TestCase]
        public void EqualsOperator()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            Assert.IsTrue(options1.FakeRpRestVersions().FakeResourceVersion == options2.FakeRpRestVersions().FakeResourceVersion);
        }

        [TestCase]
        public void EqualsOperatorString()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsTrue(FakeResourceVersions.Default == options.FakeRpRestVersions().FakeResourceVersion);
        }

        [TestCase]
        public void EqualsOperatorStringFirstNull()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsFalse(null == options.FakeRpRestVersions().FakeResourceVersion);
        }

        [TestCase]
        public void EqualsOperatorStringSecondNull()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsFalse(options.FakeRpRestVersions().FakeResourceVersion == null);
        }

        [TestCase]
        public void EqualsOperatorStringBothNull()
        {
            FakeResourceVersions v1 = null;

            Assert.IsTrue(v1 == null);
        }

        [TestCase]
        public void NotEqualsOperator()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            Assert.IsFalse(options1.FakeRpRestVersions().FakeResourceVersion != options2.FakeRpRestVersions().FakeResourceVersion);
        }

        [TestCase]
        public void NotEqualsOperatorStringFirstNull()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsTrue(null != options.FakeRpRestVersions().FakeResourceVersion);
        }

        [TestCase]
        public void NotEqualsOperatorStringSecondNull()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsTrue(options.FakeRpRestVersions().FakeResourceVersion != null);
        }

        [TestCase]
        public void NotEqualsOperatorStringBothNull()
        {
            FakeResourceVersions v1 = null;

            Assert.IsFalse(v1 != null);
        }

        [TestCase]
        public void EqualsMethod()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            Assert.IsTrue(options1.FakeRpRestVersions().FakeResourceVersion.Equals(options2.FakeRpRestVersions().FakeResourceVersion));
        }

        [TestCase]
        public void EqualsMethodVersionNull()
        {
            ArmClientOptions options = new ArmClientOptions();
            FakeResourceVersions version = null;
            Assert.IsFalse(options.FakeRpRestVersions().FakeResourceVersion.Equals(version));
        }

        [TestCase]
        public void EqualsMethodStringNull()
        {
            ArmClientOptions options = new ArmClientOptions();
            string version = null;
            Assert.IsFalse(options.FakeRpRestVersions().FakeResourceVersion.Equals(version));
        }

        [TestCase]
        public void EqualsMethodAsObject()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            object obj = options2.FakeRpRestVersions().FakeResourceVersion;
            Assert.IsTrue(options1.FakeRpRestVersions().FakeResourceVersion.Equals(obj));
        }

        [TestCase]
        public void EqualsMethodAsObjectThatIsString()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            object obj = options2.FakeRpRestVersions().FakeResourceVersion.ToString();
            Assert.IsTrue(options1.FakeRpRestVersions().FakeResourceVersion.Equals(obj));
        }

        [TestCase]
        public void EqualsMethodAsObjectThatIsInt()
        {
            ArmClientOptions options = new ArmClientOptions();

            object obj = 1;
            Assert.IsFalse(options.FakeRpRestVersions().FakeResourceVersion.Equals(obj));
        }

        [TestCase]
        public void ImplicitToString()
        {
            ArmClientOptions options = new ArmClientOptions();
            options.FakeRpRestVersions().FakeResourceVersion = FakeResourceVersions.V2019_12_01;
            string version = options.FakeRpRestVersions().FakeResourceVersion;
            Assert.IsTrue(version == "2019-12-01");
        }

        [TestCase(-1, "2019-12-01", "2020-06-01")]
        [TestCase(0, "2019-12-01", "2019-12-01")]
        [TestCase(1, "2020-06-01", "2019-12-01")]
        [TestCase(1, "2020-06-01", null)]
        public void CompareToMethodString(int expected, string version1, string version2)
        {
            FakeResourceVersions v1 = version1;
            Assert.AreEqual(expected, v1.CompareTo(version2));
        }

        [TestCase(-1, "2019-12-01", "2020-06-01")]
        [TestCase(0, "2019-12-01", "2019-12-01")]
        [TestCase(1, "2020-06-01", "2019-12-01")]
        [TestCase(1, "2020-06-01", null)]
        public void CompareToMethodVersionObject(int expected, string version1, string version2)
        {
            FakeResourceVersions v1 = version1;
            FakeResourceVersions v2 = null;
            if (version2 != null)
                v2 = version2;
            Assert.AreEqual(expected, v1.CompareTo(v2));
        }

        [TestCase]
        public void CreateFromNull()
        {
            string version = null;
            FakeResourceVersions versionObject = version;
            Assert.IsNull(versionObject);
        }

        [TestCase]
        public void CreateFromInvalidVersion()
        {
            string version = "1900-01-01";
            Assert.Throws<ArgumentException>(() =>
            {
                FakeResourceVersions versionObject = version;
            });
        }

        [TestCase]
        public void ToStringTest()
        {
            Assert.AreEqual("2020-06-01", FakeResourceVersions.Default.ToString());
        }

        [TestCase]
        public void GetHashCodeTest()
        {
            FakeResourceVersions version = FakeResourceVersions.Default;
            Assert.AreEqual(version.ToString().GetHashCode(), version.GetHashCode());
        }
    }
}
