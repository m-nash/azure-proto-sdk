using azure_proto_core;

namespace azure_proto_network
{
    public class XVirtualNetwork : VirtualNetworkOperations
    {
        public XVirtualNetwork(ArmClientContext context, PhVirtualNetwork resource, ArmClientOptions clientOptions) : base(context, resource.Id, clientOptions)
        {
            Model = resource;
        }

        public override SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientContext, Model, this.ClientOptions);
        }

        public PhVirtualNetwork Model { get; private set; }
    }
}
