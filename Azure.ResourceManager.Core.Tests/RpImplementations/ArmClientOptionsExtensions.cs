namespace Azure.ResourceManager.Core.Tests
{
    public static class ArmClientOptionsExtensions
    {
        public static FakeRpApiVersions FakeRpApiVersions(this AzureResourceManagerClientOptions armClientOptions)
        {
            return armClientOptions.GetOverrideObject<FakeRpApiVersions>(() => new FakeRpApiVersions()) as FakeRpApiVersions;
        }
    }
}
