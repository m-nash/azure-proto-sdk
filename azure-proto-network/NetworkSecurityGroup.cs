using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System.Threading;

namespace azure_proto_network
{
    public class NetworkSecurityGroup : NetworkSecurityGroupOperations
    {
        public NetworkSecurityGroup(ResourceOperationsBase operations, NetworkSecurityGroupData resource)
            : base(operations, resource.Id)
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
