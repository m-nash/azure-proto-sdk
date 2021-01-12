using Azure.ResourceManager.Core;

namespace azure_proto_compute.Extensions
{
    /// <summary>
    /// A class representing an AzureResourceManagerClientOptionsExtensions along with the instance operations that can be performed on it.
    /// </summary>
    public static class AzureResourceManagerClientOptionsExtensions
    {
        /// <summary>
        /// Adds a method to AzureResourceManagerClientOptions called ComputeRestVersions which returns all the versions to all resources inside the compute resource provider.
        /// </summary>
        /// <param name="AzureResourceManagerClientOptions"> The client parameters to use in these operations. </param>
        /// <returns> Returns a response with the <see cref="ComputeRestApiVersions"/> operation for this resource. </returns>
        public static ComputeRestApiVersions ComputeRestVersions(this AzureResourceManagerClientOptions AzureResourceManagerClientOptions)
        {
            return AzureResourceManagerClientOptions.GetOverrideObject<ComputeRestApiVersions>(() => new ComputeRestApiVersions()) as ComputeRestApiVersions;
        }
    }
}
