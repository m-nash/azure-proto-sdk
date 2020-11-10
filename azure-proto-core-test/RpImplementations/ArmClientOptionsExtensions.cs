using azure_proto_core;

namespace azure_proto_core_test
{
    public static class ArmClientOptionsExtensions
    {
        public static FakeRpApiVersions FakeRpApiVersions(this ArmClientOptions armClientOptions)
        {
            return armClientOptions.GetOverrideObject<FakeRpApiVersions>(() => new FakeRpApiVersions()) as FakeRpApiVersions;
        }
    }
}
