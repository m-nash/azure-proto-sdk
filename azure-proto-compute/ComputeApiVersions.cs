namespace azure_proto_compute
{
    public class ComputeRestVersions
    {
        internal ComputeRestVersions()
        {
            VirtualMachinesVersion = VirtualMachinesApiVersions.Default;
            AvailabilitySetsVersion = AvailabilitySetsApiVersions.Default;
        }

        public VirtualMachinesApiVersions VirtualMachinesVersion { get; set; }

        public AvailabilitySetsApiVersions AvailabilitySetsVersion { get; set; }
    }
}
