using azure_proto_core;

namespace azure_proto_compute.Extensions
{
    public static class ArmClientOptionsExtensions
    {
        public static ComputeRestVersions ComputeRestVersions(this ArmClientOptions armClientOptions)
        {
            return armClientOptions.GetOverrideObject<ComputeRestVersions>(() => new ComputeRestVersions()) as ComputeRestVersions;
        }
    }
}
