using azure_proto_core;

namespace azure_proto_compute
{
    public class VirtualMachinesApiVersions : ApiVersionsBase
    {
        public static readonly VirtualMachinesApiVersions V2020_06_01 = new VirtualMachinesApiVersions("2020-06-01");
        public static readonly VirtualMachinesApiVersions V2019_12_01 = new VirtualMachinesApiVersions("2019-12-01");
        public static readonly VirtualMachinesApiVersions Default = V2020_06_01;

        static VirtualMachinesApiVersions()
        {
            _validValues.Add(V2020_06_01);
            _validValues.Add(V2019_12_01);
        }

        private VirtualMachinesApiVersions(string value) : base(value) { }

        public static implicit operator string(VirtualMachinesApiVersions version)
        {
            if (version == null)
                return null;
            return version.ToString();
        }
    }
}
