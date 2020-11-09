using azure_proto_core;
using NUnit.Framework;

namespace azure_proto_core_test
{
    public class ApiVersionsBaseTests
    {
        [TestCase]
        public void VersionToString()
        {
            ArmClientOptions options = new ArmClientOptions();
            Assert.AreEqual("2020-06-01", options.FakeRpApiVersions().FakeResourceVersion);
        }

        [TestCase]
        public void EqualsOperator()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            Assert.IsTrue(options1.FakeRpApiVersions().FakeResourceVersion == options2.FakeRpApiVersions().FakeResourceVersion);
        }

        [TestCase]
        public void EqualsOperatorString()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsTrue(FakeResourceApiVersions.Default == options.FakeRpApiVersions().FakeResourceVersion);
        }

        [TestCase]
        public void EqualsOperatorStringFirstNull()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsFalse(null == options.FakeRpApiVersions().FakeResourceVersion);
        }

        [TestCase]
        public void EqualsOperatorStringSecondNull()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsFalse(options.FakeRpApiVersions().FakeResourceVersion == null);
        }

        [TestCase]
        public void EqualsOperatorStringBothNull()
        {
            FakeResourceApiVersions v1 = null;

            Assert.IsTrue(v1 == null);
        }

        [TestCase]
        public void NotEqualsOperator()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            Assert.IsFalse(options1.FakeRpApiVersions().FakeResourceVersion != options2.FakeRpApiVersions().FakeResourceVersion);
        }

        [TestCase]
        public void NotEqualsOperatorStringFirstNull()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsTrue(null != options.FakeRpApiVersions().FakeResourceVersion);
        }

        [TestCase]
        public void NotEqualsOperatorStringSecondNull()
        {
            ArmClientOptions options = new ArmClientOptions();

            Assert.IsTrue(options.FakeRpApiVersions().FakeResourceVersion != null);
        }

        [TestCase]
        public void NotEqualsOperatorStringBothNull()
        {
            FakeResourceApiVersions v1 = null;

            Assert.IsFalse(v1 != null);
        }

        [TestCase]
        public void EqualsMethod()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            Assert.IsTrue(options1.FakeRpApiVersions().FakeResourceVersion.Equals(options2.FakeRpApiVersions().FakeResourceVersion));
        }

        [TestCase]
        public void EqualsMethodVersionNull()
        {
            ArmClientOptions options = new ArmClientOptions();
            FakeResourceApiVersions version = null;
            Assert.IsFalse(options.FakeRpApiVersions().FakeResourceVersion.Equals(version));
        }

        [TestCase]
        public void EqualsMethodStringNull()
        {
            ArmClientOptions options = new ArmClientOptions();
            string version = null;
            Assert.IsFalse(options.FakeRpApiVersions().FakeResourceVersion.Equals(version));
        }

        [TestCase]
        public void EqualsMethodAsObject()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            object obj = options2.FakeRpApiVersions().FakeResourceVersion;
            Assert.IsTrue(options1.FakeRpApiVersions().FakeResourceVersion.Equals(obj));
        }

        [TestCase]
        public void EqualsMethodAsObjectThatIsString()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            object obj = options2.FakeRpApiVersions().FakeResourceVersion.ToString();
            Assert.IsTrue(options1.FakeRpApiVersions().FakeResourceVersion.Equals(obj));
        }

        [TestCase]
        public void EqualsMethodAsObjectThatIsInt()
        {
            ArmClientOptions options = new ArmClientOptions();

            object obj = 1;
            Assert.IsFalse(options.FakeRpApiVersions().FakeResourceVersion.Equals(obj));
        }

        [TestCase]
        public void ImplicitToString()
        {
            ArmClientOptions options = new ArmClientOptions();
            options.FakeRpApiVersions().FakeResourceVersion = FakeResourceApiVersions.V2019_12_01;
            string version = options.FakeRpApiVersions().FakeResourceVersion;
            Assert.IsTrue(version == "2019-12-01");
        }

        [TestCase(-1, "2019-12-01", "2020-06-01")]
        [TestCase(0, "2019-12-01", "2019-12-01")]
        [TestCase(1, "2020-06-01", "2019-12-01")]
        [TestCase(1, "2020-06-01", null)]
        public void CompareToMethodString(int expected, string version1, string version2)
        {
            FakeResourceApiVersions v1 = version1 == "2019-12-01" ? FakeResourceApiVersions.V2019_12_01 : FakeResourceApiVersions.V2020_06_01;
            Assert.AreEqual(expected, v1.CompareTo(version2));
        }

        private FakeResourceApiVersions ConvertFromString(string version)
        {
            return version == "2019-12-01" ? FakeResourceApiVersions.V2019_12_01 : FakeResourceApiVersions.V2020_06_01;
        }

        [TestCase(-1, "2019-12-01", "2020-06-01")]
        [TestCase(0, "2019-12-01", "2019-12-01")]
        [TestCase(1, "2020-06-01", "2019-12-01")]
        [TestCase(1, "2020-06-01", null)]
        public void CompareToMethodVersionObject(int expected, string version1, string version2)
        {
            FakeResourceApiVersions v1 = ConvertFromString(version1);
            FakeResourceApiVersions v2 = null;
            if (version2 != null)
                v2 = ConvertFromString(version2);
            Assert.AreEqual(expected, v1.CompareTo(v2));
        }

        [TestCase]
        public void ToStringTest()
        {
            Assert.AreEqual("2020-06-01", FakeResourceApiVersions.Default.ToString());
        }

        [TestCase]
        public void GetHashCodeTest()
        {
            FakeResourceApiVersions version = FakeResourceApiVersions.Default;
            Assert.AreEqual(version.ToString().GetHashCode(), version.GetHashCode());
        }
    }
}
