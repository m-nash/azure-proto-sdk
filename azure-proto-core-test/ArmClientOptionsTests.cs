using azure_proto_core;
using NUnit.Framework;

namespace azure_proto_core_test
{
    public class ArmClientOptionsTests
    {
        [TestCase]
        public void VersionIsDefault()
        {
            ArmClientOptions options = new ArmClientOptions();
            Assert.AreEqual(FakeResourceVersions.Default, options.FakeRpRestVersions().FakeResourceVersion);
        }

        [TestCase]
        public void MultiClientSeparateVersions()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            options2.FakeRpRestVersions().FakeResourceVersion = FakeResourceVersions.V2019_12_01;
            Assert.AreEqual(FakeResourceVersions.Default, options1.FakeRpRestVersions().FakeResourceVersion);
            Assert.AreEqual(FakeResourceVersions.V2019_12_01, options2.FakeRpRestVersions().FakeResourceVersion);
        }
    }
}
