using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace azure_proto_network
{
    public class XNetworkSecurityGroup : NetworkSecurityGroupOperations
    {
        public XNetworkSecurityGroup(ArmClientContext context, PhNetworkSecurityGroup resource) : base(context, resource)
        {
            Model = resource;
        }

        public PhNetworkSecurityGroup Model { get; private set; }

        public ArmOperation<NetworkSecurityGroupOperations> UpdateRules(CancellationToken cancellationToken = default, params SecurityRule[] rules)
        {
            return UpdateRules(Model, cancellationToken, rules);
        }
    }
}
