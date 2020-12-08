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
    public class SubnetOperations : ResourceOperationsBase<Subnet, SubnetData>, IDeletableResource<Subnet, SubnetData>
    {
        public SubnetOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions) : base(context, id, clientOptions) { }

        public SubnetOperations(ArmClientContext context, azure_proto_core.Resource resource, ArmClientOptions clientOptions) : base(context, resource, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";
        
        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                    ArmClientOptions.Convert<NetworkManagementClientOptions>(ClientOptions))).Subnets;

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Parent.Name, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, cancellationToken));
        }

        public override ArmResponse<Subnet> Get()
        {
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(Operations.Get(base.Id.ResourceGroup, base.Id.Parent.Name, base.Id.Name),
                n => { base.Resource = new SubnetData(n, base.DefaultLocation); return new Subnet(base.ClientContext, base.Resource as SubnetData, ClientOptions); });
        }

        public async override Task<ArmResponse<Subnet>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(await Operations.GetAsync(base.Id.ResourceGroup, base.Id.Parent.Name, base.Id.Name, null, cancellationToken),
                n => { base.Resource = new SubnetData(n, base.DefaultLocation); return new Subnet(base.ClientContext, base.Resource as SubnetData, ClientOptions); });
        }
    }
}
