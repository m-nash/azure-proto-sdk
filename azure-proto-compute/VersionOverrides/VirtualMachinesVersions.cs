using azure_proto_core;

namespace azure_proto_compute
{
    public class VirtualMachinesVersions : ApiVersionsBase
    {
        public static readonly string V2020_06_01 = "2020-06-01";
        public static readonly string V2019_12_01 = "2019-12-01";
        public static readonly string Default = V2020_06_01;

        static VirtualMachinesVersions()
        {
            _validValues.Add(V2020_06_01);
            _validValues.Add(V2019_12_01);
        }

        private VirtualMachinesVersions(string value) : base(value) { }

        public static implicit operator string(VirtualMachinesVersions version)
        {
            if (version == null)
                return null;
            return version.ToString();
        }

        public static implicit operator VirtualMachinesVersions(string value)
        {
            if (value == null)
                return null;
            return new VirtualMachinesVersions(value);
        }
    }
}
