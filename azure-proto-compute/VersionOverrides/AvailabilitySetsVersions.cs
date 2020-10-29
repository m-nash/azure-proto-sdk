using azure_proto_core;

namespace azure_proto_compute
{
    public class AvailabilitySetsVersions : ApiVersionsBase
    {
        public static readonly string V2020_05_01 = "2020-05-01";
        public static readonly string V2019_12_01 = "2019-12-01";
        public static readonly string Default = V2020_05_01;

        static AvailabilitySetsVersions()
        {
            _validValues.Add(V2020_05_01);
            _validValues.Add(V2019_12_01);
        }

        private AvailabilitySetsVersions(string value) : base(value) { }

        public static implicit operator string(AvailabilitySetsVersions version)
        {
            if (version == null)
                return null;
            return version.ToString();
        }

        public static implicit operator AvailabilitySetsVersions(string value)
        {
            if (value == null)
                return null;
            return new AvailabilitySetsVersions(value);
        }
    }
}
