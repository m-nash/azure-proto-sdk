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
    public class SubnetOperations : ResourceOperations<SubnetOperations, PhSubnet>, IDeletableResource<SubnetOperations, PhSubnet>
    {
        public SubnetOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public SubnetOperations(ArmClientContext context, azure_proto_core.Resource resource) : base(context, resource) { }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Parent.Name, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, cancellationToken));
        }

        public override ArmResponse<SubnetOperations> Get()
        {
            return new PhArmResponse<SubnetOperations, Subnet>(Operations.Get(Id.ResourceGroup, Id.Parent.Name, Id.Name),
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }

        public async override Task<ArmResponse<SubnetOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<SubnetOperations, Subnet>(await Operations.GetAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, null, cancellationToken),
                n => { Resource = new PhSubnet(n, DefaultLocation); return this; });
        }


        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).Subnets;
    }
}
