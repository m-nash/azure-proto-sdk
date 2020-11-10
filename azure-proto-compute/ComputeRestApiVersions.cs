namespace azure_proto_compute
{
    public class ComputeRestApiVersions
    {
        internal ComputeRestApiVersions()
        {
            VirtualMachinesVersion = VirtualMachinesApiVersions.Default;
            AvailabilitySetsVersion = AvailabilitySetsApiVersions.Default;
        }

        public VirtualMachinesApiVersions VirtualMachinesVersion { get; set; }

        public AvailabilitySetsApiVersions AvailabilitySetsVersion { get; set; }
    }
}
