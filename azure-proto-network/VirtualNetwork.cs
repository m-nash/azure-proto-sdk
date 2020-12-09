using azure_proto_core;

namespace azure_proto_network
{
    public class VirtualNetwork : VirtualNetworkOperations
    {
        public VirtualNetwork(ArmClientContext context, VirtualNetworkData resource, ArmClientOptions options)
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
