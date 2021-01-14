using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class VirtualNetwork : VirtualNetworkOperations
    {
        public VirtualNetwork(ResourceOperationsBase options, VirtualNetworkData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        public VirtualNetworkData Data { get; private set; }
    }
}
