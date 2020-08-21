using azure_proto_core;

namespace azure_proto_network
{
    public class VnetCollection : ResourceCollectionOperations<PhVirtualNetwork>
    {
        public VnetCollection(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VnetCollection(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        protected override ResourceOperations<PhVirtualNetwork> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new VnetOperations(this, resource);
        }
    }

}
