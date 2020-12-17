using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class VirtualNetwork : VirtualNetworkOperations
    {
        public VirtualNetwork(AzureResourceManagerClientContext context, VirtualNetworkData resource)
            : base(context, resource.Id)
        {
            Data = resource;
        }

        public override SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientContext, Data);
        }

        public VirtualNetworkData Data { get; private set; }
    }
}
