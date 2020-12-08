using azure_proto_core;

namespace azure_proto_network
{
    public class VirtualNetwork : VirtualNetworkOperations
    {
        public VirtualNetwork(ArmClientContext context, VirtualNetworkData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Model = resource;
        }

        public override SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientContext, Model, ClientOptions);
        }

        public VirtualNetworkData Model { get; private set; }
    }
}
