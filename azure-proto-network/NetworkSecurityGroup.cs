using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;

namespace azure_proto_network
{
    public class NetworkSecurityGroup : NetworkSecurityGroupOperations
    {
        public NetworkSecurityGroup(ArmClientContext context, NetworkSecurityGroupData resource) : base(context, resource.Id)
        {
            Model = resource;
        }

        public NetworkSecurityGroupData Model { get; private set; }

        public ArmOperation<NetworkSecurityGroup> UpdateRules(CancellationToken cancellationToken = default, params SecurityRule[] rules)
        {
            return UpdateRules(Model, cancellationToken, rules);
        }
    }
}
