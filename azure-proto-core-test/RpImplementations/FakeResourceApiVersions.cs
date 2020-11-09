using azure_proto_core;

namespace azure_proto_core_test
{
    public class FakeResourceApiVersions : ApiVersionsBase
    {
        public static readonly FakeResourceApiVersions V2020_06_01 = new FakeResourceApiVersions("2020-06-01");
        public static readonly FakeResourceApiVersions V2019_12_01 = new FakeResourceApiVersions("2019-12-01");
        public static readonly FakeResourceApiVersions Default = V2020_06_01;

        private FakeResourceApiVersions(string value) : base(value) { }

        public static implicit operator string(FakeResourceApiVersions version)
        {
            if (version == null)
                return null;
            return version.ToString();
        }
    }
}
