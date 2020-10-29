using azure_proto_core;

namespace azure_proto_core_test
{
    public class FakeResourceVersions : ApiVersionsBase
    {
        public static readonly string V2020_06_01 = "2020-06-01";
        public static readonly string V2019_12_01 = "2019-12-01";
        public static readonly string Default = V2020_06_01;

        static FakeResourceVersions()
        {
            _validValues.Add(V2020_06_01);
            _validValues.Add(V2019_12_01);
        }

        private FakeResourceVersions(string value) : base(value) { }

        public static implicit operator string(FakeResourceVersions version)
        {
            if (version == null)
                return null;
            return version.ToString();
        }

        public static implicit operator FakeResourceVersions(string value)
        {
            if (value == null)
                return null;
            return new FakeResourceVersions(value);
        }
    }
}
