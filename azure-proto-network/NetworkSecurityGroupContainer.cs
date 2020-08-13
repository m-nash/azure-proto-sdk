using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NsgContainer : ArmResourceContainerOperations<PhNetworkSecurityGroup, Operation<PhNetworkSecurityGroup>>
    {
        public NsgContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NsgContainer(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).NetworkSecurityGroups;

        public override Operation<PhNetworkSecurityGroup> Create(string name, PhNetworkSecurityGroup resourceDetails)
        {
            return new PhValueOperation<PhNetworkSecurityGroup, NetworkSecurityGroup>(Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, resourceDetails.Model), n => new PhNetworkSecurityGroup(n));
        }

        public async override Task<Operation<PhNetworkSecurityGroup>> CreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhValueOperation<PhNetworkSecurityGroup, NetworkSecurityGroup>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Name, resourceDetails.Model, cancellationToken), n => new PhNetworkSecurityGroup(n));
        }
    }
}
