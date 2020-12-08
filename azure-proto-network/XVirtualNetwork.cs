using azure_proto_core;

namespace azure_proto_network
{
    public class XVirtualNetwork : VirtualNetworkOperations
    {
        public XVirtualNetwork(ArmClientContext context, PhVirtualNetwork resource) : base(context, resource.Id)
        {
            Model = resource;
        }

        public override SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientContext, Model);
        }

        public PhVirtualNetwork Model { get; private set; }
    }
}
