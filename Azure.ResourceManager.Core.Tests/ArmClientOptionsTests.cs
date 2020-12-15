﻿using NUnit.Framework;

namespace Azure.ResourceManager.Core.Tests
{
    public class ArmClientOptionsTests
    {
        [TestCase]
        public void VersionIsDefault()
        {
            ArmClientOptions options = new ArmClientOptions();
            Assert.AreEqual(FakeResourceApiVersions.Default, options.FakeRpApiVersions().FakeResourceVersion);
        }

        [TestCase]
        public void MultiClientSeparateVersions()
        {
            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();

            options2.FakeRpApiVersions().FakeResourceVersion = FakeResourceApiVersions.V2019_12_01;
            Assert.AreEqual(FakeResourceApiVersions.Default, options1.FakeRpApiVersions().FakeResourceVersion);
            Assert.AreEqual(FakeResourceApiVersions.V2019_12_01, options2.FakeRpApiVersions().FakeResourceVersion);
        }
    }
}
