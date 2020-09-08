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
    public class SubnetContainer : ResourceContainerOperations<PhSubnet>
    {
        public SubnetContainer(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetContainer(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";


        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).Subnets;

        public override ArmOperation<ResourceClientBase<PhSubnet>> Create(string name, PhSubnet resourceDetails)
        {
            var operation = Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, name, resourceDetails.Model);
            return new PhArmOperation<ResourceClientBase<PhSubnet>, Subnet>(operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(), 
                s => Subnet(new PhSubnet(s, Location.Default)));
        }

        public async override Task<ArmOperation<ResourceClientBase<PhSubnet>>> CreateAsync(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceClientBase<PhSubnet>, Subnet>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Name, name, resourceDetails.Model, cancellationToken),
                s => Subnet(new PhSubnet(s, Location.Default)));
        }

        internal SubnetOperations Subnet(PhSubnet subnet)
        {
            return new SubnetOperations(this, subnet);
        }
    }
}
