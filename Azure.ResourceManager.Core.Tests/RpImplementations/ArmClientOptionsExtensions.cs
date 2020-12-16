namespace Azure.ResourceManager.Core.Tests
{
    public static class ArmClientOptionsExtensions
    {
        public static FakeRpApiVersions FakeRpApiVersions(this ArmClientOptions armClientOptions)
        {
            return armClientOptions.GetOverrideObject<FakeRpApiVersions>(() => new FakeRpApiVersions()) as FakeRpApiVersions;
        }
    }
}
