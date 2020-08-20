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
    public class SubnetOperations : ResourceOperations<PhSubnet, Subnet, ArmOperation<PhSubnet>, ArmOperation<Response>>
    {
        public SubnetOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetOperations(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";


        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Context.ResourceGroup, Context.Parent.Name, Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Context.ResourceGroup, Context.Parent.Name, Context.Name, cancellationToken));
        }

        public override Response<PhSubnet> Get()
        {
            return new PhResponse<PhSubnet, Subnet>(Operations.Get(Context.ResourceGroup, Context.Parent.Name, Context.Name), n => new PhSubnet(n, Location.Default));
        }

        public async override Task<Response<PhSubnet>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhSubnet, Subnet>(await Operations.GetAsync(Context.ResourceGroup, Context.Parent.Name, Context.Name, null, cancellationToken), n => new PhSubnet(n, Location.Default));
        }

        public override ArmOperation<PhSubnet> Update(Subnet patchable)
        {
            return new PhArmOperation<PhSubnet, Subnet>(Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Parent.Name, Context.Name, patchable), n => new PhSubnet(n, Location.Default));
        }

        public async override Task<ArmOperation<PhSubnet>> UpdateAsync(Subnet patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PhSubnet, Subnet>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Parent.Name, Context.Name, patchable, cancellationToken), n => new PhSubnet(n, Location.Default));
        }

        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).Subnets;

    }
}
