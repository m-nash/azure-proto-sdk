using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class VirtualNetwork : VirtualNetworkOperations
    {
        public VirtualNetwork(AzureResourceManagerClientContext context, VirtualNetworkData resource, AzureResourceManagerClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public override SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientContext, Data, ClientOptions);
        }

        public VirtualNetworkData Data { get; private set; }
    }
}
