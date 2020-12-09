using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;

namespace azure_proto_network
{
    public class NetworkSecurityGroup : NetworkSecurityGroupOperations
    {
        public NetworkSecurityGroup(ArmClientContext context, NetworkSecurityGroupData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public NetworkSecurityGroupData Data { get; private set; }

        public ArmOperation<NetworkSecurityGroup> UpdateRules(CancellationToken cancellationToken = default, params SecurityRule[] rules)
        {
            return UpdateRules(Data, cancellationToken, rules);
        }
    }
}
