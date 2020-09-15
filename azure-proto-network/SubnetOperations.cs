using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
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
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Parent.Name, Id.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, cancellationToken));
        }

        public override ArmResponse<ResourceOperationsBase<PhSubnet>> Get()
        {
            return new PhArmResponse<ResourceOperationsBase<PhSubnet>, Subnet>(Operations.Get(Id.ResourceGroup, Id.Parent.Name, Id.Name), 
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }

        public async override Task<ArmResponse<ResourceOperationsBase<PhSubnet>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperationsBase<PhSubnet>, Subnet>(await Operations.GetAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, null, cancellationToken), 
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }

        public override ArmOperation<ResourceOperationsBase<PhSubnet>> AddTag(string key, string value)
        {
            Subnet patchable = new Subnet();
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Parent.Name, Id.Name, patchable), 
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhSubnet>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            Subnet patchable = new Subnet();
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, patchable, cancellationToken),
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }

        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).Subnets;
    }
}
