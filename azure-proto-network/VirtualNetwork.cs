using azure_proto_core;

namespace azure_proto_network
{
    public class VirtualNetwork : VirtualNetworkOperations
    {
        public VirtualNetwork(ArmClientContext context, VirtualNetworkData resource) : base(context, resource.Id)
        {
            Model = resource;
        }

        public override SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientContext, Model);
        }

        public VirtualNetworkData Model { get; private set; }
    }
}
