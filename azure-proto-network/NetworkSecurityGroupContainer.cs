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
        public NetworkSecurityGroupContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkSecurityGroupContainer(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }
        public NetworkSecurityGroupContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkSecurityGroupContainer(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).NetworkSecurityGroups;

        public override ArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>> Create(string name, PhNetworkSecurityGroup resourceDetails)
        {
            return new PhArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>, NetworkSecurityGroup>(Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, resourceDetails.Model), 
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>>> CreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>, NetworkSecurityGroup>(
                await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Name, resourceDetails.Model, cancellationToken),
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }
    }
}
