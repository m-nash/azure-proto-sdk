namespace azure_proto_core_test
{
    public class FakeRpApiVersions
    {
        internal FakeRpApiVersions()
        {
            FakeResourceVersion = FakeResourceApiVersions.Default;
        }

        public FakeResourceApiVersions FakeResourceVersion { get; set; }
    }
}
