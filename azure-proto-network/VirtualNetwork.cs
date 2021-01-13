using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    /// <summary>
    /// A class representing a virtual nerwork along with the instance operations that can be performed on it.
    /// </summary>
    public class VirtualNetwork : VirtualNetworkOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualNetwork"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The resource that is the target of operations. </param>
        public VirtualNetwork(AzureResourceManagerClientOptions options, VirtualNetworkData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        /// <inheritdoc/>
        public override SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientOptions, Data);
        }

        /// <summary>
        /// Gets or sets the virtual nerwork data.
        /// </summary>
        public VirtualNetworkData Data { get; private set; }
    }
}
