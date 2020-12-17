using Azure.ResourceManager.Core;

namespace azure_proto_compute.Extensions
{
    public static class AzureResourceManagerClientOptionsExtensions
    {
        public static ComputeRestApiVersions ComputeRestVersions(this AzureResourceManagerClientOptions AzureResourceManagerClientOptions)
        {
            return AzureResourceManagerClientOptions.GetOverrideObject<ComputeRestApiVersions>(() => new ComputeRestApiVersions()) as ComputeRestApiVersions;
        }
    }
}
