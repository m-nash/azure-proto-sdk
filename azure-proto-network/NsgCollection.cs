using azure_proto_core;

namespace azure_proto_network
{
    public class NsgCollection : ResourceCollectionOperations<PhNetworkSecurityGroup>
    {
        public NsgCollection(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NsgCollection(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        protected override ResourceOperations<PhNetworkSecurityGroup> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new NsgOperations(this, resource);
        }
    }
}
