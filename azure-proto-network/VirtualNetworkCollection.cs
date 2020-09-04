using azure_proto_core;

namespace azure_proto_network
{
    public class VirtualNetworkCollection : ResourceCollectionOperations<PhVirtualNetwork>
    {
        public VirtualNetworkCollection(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualNetworkCollection(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        protected override ResourceClientBase<PhVirtualNetwork> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new VirtualNetworkOperations(this, resource);
        }
    }

}
