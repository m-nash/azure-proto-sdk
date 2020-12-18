using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
    /// </summary>
    public class SubnetOperations : ResourceOperationsBase<Subnet>, IDeletableResource
    {
        internal SubnetOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        internal SubnetOperations(AzureResourceManagerClientOptions options, Resource resource)
            : base(options, resource)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";

        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                    ClientOptions.Convert<NetworkManagementClientOptions>())).Subnets;

        public ArmResponse<Response> Delete()
        {
            return new ArmVoidResponse(Operations.StartDelete(Id.ResourceGroup, Id.Parent.Name, Id.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidResponse((await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name)).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public ArmOperation<Response> StartDelete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Parent.Name, Id.Name));
        }

        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, cancellationToken));
        }

        public override ArmResponse<Subnet> Get()
        {
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(Operations.Get(Id.ResourceGroup, Id.Parent.Name, Id.Name),
                n =>
                {
                    Resource = new SubnetData(n, DefaultLocation);
                    return new Subnet(ClientOptions, Resource as SubnetData);
                });
        }

        public async override Task<ArmResponse<Subnet>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(await Operations.GetAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, null, cancellationToken),
                n =>
                {
                    Resource = new SubnetData(n, DefaultLocation);
                    return new Subnet(ClientOptions, Resource as SubnetData);
                });
        }
    }
}
