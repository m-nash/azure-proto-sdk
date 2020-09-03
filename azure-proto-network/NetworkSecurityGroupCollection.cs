using azure_proto_core;

namespace azure_proto_network
{
    public class NetworkSecurityGroupCollection : ResourceCollectionOperations<PhNetworkSecurityGroup>
    {
        public NetworkSecurityGroupCollection(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkSecurityGroupCollection(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        protected override ResourceClientBase<PhNetworkSecurityGroup> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new NetworkSecurityGroupOperations(this, resource);
        }
    }
}
