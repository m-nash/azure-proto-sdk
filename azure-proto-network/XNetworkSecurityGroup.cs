using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;

namespace azure_proto_network
{
    public class XNetworkSecurityGroup : NetworkSecurityGroupOperations
    {
        internal XNetworkSecurityGroup(ArmClientContext context, PhNetworkSecurityGroup resource, ArmClientOptions clientOptions)
            : base(context, resource.Id, clientOptions)
        {
            Model = resource;
        }

        public PhNetworkSecurityGroup Model { get; private set; }

        public ArmOperation<XNetworkSecurityGroup> UpdateRules(CancellationToken cancellationToken = default, params SecurityRule[] rules)
        {
            return UpdateRules(Model, cancellationToken, rules);
        }
    }
}
