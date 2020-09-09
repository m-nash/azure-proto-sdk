using azure_proto_core;

namespace azure_proto_network
{
    public class VirtualNetworkCollection : ResourceCollectionOperations<PhVirtualNetwork>
    {
        public VirtualNetworkCollection(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualNetworkCollection(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public VirtualNetworkCollection(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualNetworkCollection(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        protected override ResourceOperationsBase<PhVirtualNetwork> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new VirtualNetworkOperations(this, resource);
        }
    }

}
