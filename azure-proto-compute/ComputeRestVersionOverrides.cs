namespace azure_proto_compute
{
    public class ComputeRestVersions
    {
        internal ComputeRestVersions()
        {
            VirtualMachinesVersion = VirtualMachinesVersions.Default;
            AvailabilitySetsVersion = AvailabilitySetsVersions.Default;
        }

        public VirtualMachinesVersions VirtualMachinesVersion { get; set; }

        public AvailabilitySetsVersions AvailabilitySetsVersion { get; set; }
    }
}
