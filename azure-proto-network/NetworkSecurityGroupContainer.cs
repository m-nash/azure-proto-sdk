using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkSecurityGroupContainer : ResourceContainerOperations<PhNetworkSecurityGroup>
    {
        public NetworkSecurityGroupContainer(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkSecurityGroupContainer(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).NetworkSecurityGroups;

        public override ArmOperation<ResourceClientBase<PhNetworkSecurityGroup>> Create(string name, PhNetworkSecurityGroup resourceDetails)
        {
            var operation = Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, resourceDetails.Model);
            return new PhArmOperation<ResourceClientBase<PhNetworkSecurityGroup>, NetworkSecurityGroup>(operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(), 
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }

        public async override Task<ArmOperation<ResourceClientBase<PhNetworkSecurityGroup>>> CreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceClientBase<PhNetworkSecurityGroup>, NetworkSecurityGroup>(
                await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Name, resourceDetails.Model, cancellationToken),
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }
    }
}
