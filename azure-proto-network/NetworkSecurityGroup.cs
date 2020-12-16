using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System.Threading;

namespace azure_proto_network
{
    public class NetworkSecurityGroup : NetworkSecurityGroupOperations
    {
        public NetworkSecurityGroup(AzureResourceManagerClientContext context, NetworkSecurityGroupData resource, AzureResourceManagerClientOptions options)
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
