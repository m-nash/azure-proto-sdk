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
        public SubnetContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetContainer(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public SubnetContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetContainer(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";


        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).Subnets;

        public override ArmOperation<ResourceOperationsBase<PhSubnet>> Create(string name, PhSubnet resourceDetails)
        {
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, name, resourceDetails.Model), 
                s => Subnet(new PhSubnet(s, Location.Default)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhSubnet>>> CreateAsync(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Name, name, resourceDetails.Model, cancellationToken),
                s => Subnet(new PhSubnet(s, Location.Default)));
        }

        internal SubnetOperations Subnet(PhSubnet subnet)
        {
            return new SubnetOperations(this, subnet);
        }
    }
}
