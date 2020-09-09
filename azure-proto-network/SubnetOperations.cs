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
    /// <summary>
    /// TODO: Split into ResourceOperations/TrackedResourceOperations
    /// </summary>
    public class SubnetOperations : ResourceOperationsBase<PhSubnet>
    {
        public SubnetOperations(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetOperations(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public SubnetOperations(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetOperations(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Context.ResourceGroup, Context.Parent.Name, Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Context.ResourceGroup, Context.Parent.Name, Context.Name, cancellationToken));
        }

        public override Response<ResourceOperationsBase<PhSubnet>> Get()
        {
            return new PhArmResponse<ResourceOperationsBase<PhSubnet>, Subnet>(Operations.Get(Context.ResourceGroup, Context.Parent.Name, Context.Name), 
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }

        public async override Task<Response<ResourceOperationsBase<PhSubnet>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperationsBase<PhSubnet>, Subnet>(await Operations.GetAsync(Context.ResourceGroup, Context.Parent.Name, Context.Name, null, cancellationToken), 
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }

        public override ArmOperation<ResourceOperationsBase<PhSubnet>> AddTag(string key, string value)
        {
            Subnet patchable = new Subnet();
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Parent.Name, Context.Name, patchable), 
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhSubnet>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            Subnet patchable = new Subnet();
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Parent.Name, Context.Name, patchable, cancellationToken),
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }

        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).Subnets;

    }
}
