using azure_proto_core;

namespace azure_proto_compute
{
    public class AvailabilitySetsApiVersions : ApiVersionsBase
    {
        public static readonly AvailabilitySetsApiVersions V2020_05_01 = new AvailabilitySetsApiVersions("2020-05-01");
        public static readonly AvailabilitySetsApiVersions V2019_12_01 = new AvailabilitySetsApiVersions("2019-12-01");
        public static readonly AvailabilitySetsApiVersions Default = V2020_05_01;

        static AvailabilitySetsApiVersions()
        {
            _validValues.Add(V2020_05_01);
            _validValues.Add(V2019_12_01);
        }

        private AvailabilitySetsApiVersions(string value) : base(value) { }

        public static implicit operator string(AvailabilitySetsApiVersions version)
        {
            if (version == null)
                return null;
            return version.ToString();
        }
    }
}
