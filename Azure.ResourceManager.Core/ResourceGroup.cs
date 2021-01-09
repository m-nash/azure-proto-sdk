namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing a ResourceGroup along with the instance operations that can be performed on it.
    /// </summary>
    public class ResourceGroup : ResourceGroupOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceGroup"/> class.
        /// </summary>
        /// /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The resource group data to use in these operations. </param>
        internal ResourceGroup(AzureResourceManagerClientOptions options, ResourceGroupData resource)
            : base(options, resource)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets initializes a new instance of the <see cref="ResourceGroupData"/> class.
        /// </summary>
        public ResourceGroupData Data { get; private set; }
    }
}
