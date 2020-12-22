using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class VirtualNetwork : VirtualNetworkOperations
    {
        public VirtualNetwork(AzureResourceManagerClientOptions options, VirtualNetworkData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        public override SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientOptions, Data);
        }

        public VirtualNetworkData Data { get; private set; }
    }
}
