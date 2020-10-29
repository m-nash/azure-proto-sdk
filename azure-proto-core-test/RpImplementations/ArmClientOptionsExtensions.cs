using azure_proto_core;

namespace azure_proto_core_test
{
    public static class ArmClientOptionsExtensions
    {
        public static FakeRpRestVersions FakeRpRestVersions(this ArmClientOptions armClientOptions)
        {
            return armClientOptions.GetOverrideObject<FakeRpRestVersions>(() => new FakeRpRestVersions()) as FakeRpRestVersions;
        }
    }
}
