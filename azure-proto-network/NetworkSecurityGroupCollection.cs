using azure_proto_core;

namespace azure_proto_network
{
    public class NetworkSecurityGroupCollection : ResourceCollectionOperations<PhNetworkSecurityGroup>
    {

        public NetworkSecurityGroupCollection(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkSecurityGroupCollection(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }
        public NetworkSecurityGroupCollection(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkSecurityGroupCollection(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        protected override ResourceOperationsBase<PhNetworkSecurityGroup> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new NetworkSecurityGroupOperations(this, resource);
        }
    }
}
