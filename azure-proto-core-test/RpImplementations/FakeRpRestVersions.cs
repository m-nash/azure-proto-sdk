namespace azure_proto_core_test
{
    public class FakeRpRestVersions
    {
        internal FakeRpRestVersions()
        {
            FakeResourceVersion = FakeResourceVersions.Default;
        }

        public FakeResourceVersions FakeResourceVersion { get; set; }
    }
}
