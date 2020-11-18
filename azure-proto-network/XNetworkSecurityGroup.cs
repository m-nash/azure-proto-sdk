using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;

namespace azure_proto_network
{
    public class XNetworkSecurityGroup : NetworkSecurityGroupOperations
    {
        public XNetworkSecurityGroup(ArmClientContext context, PhNetworkSecurityGroup resource) : base(context, resource.Id)
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
