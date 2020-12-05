using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Operations for the RespourceGroups container in the given subscription context.  Allows Creating and listign respource groups
    /// and provides an attachment point for Collections of Tracked Resources.
    /// </summary>
    public class ResourceGroupContainerOperations : ResourceContainerOperations<ResourceGroupOperations, PhResourceGroup>
    {
        public override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        internal ResourceGroupContainerOperations(ArmClientContext context, SubscriptionOperations subscription, ArmClientOptions clientOptions) : base(context, subscription.Id, clientOptions)
        {
        }

        public ArmOperation<ResourceGroupOperations> Create(string name, Location location)
        {
            var model = new PhResourceGroup(new ResourceGroup(location));
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(
                Operations.CreateOrUpdate(name, model),
                g => new ResourceGroupOperations(ClientContext, new PhResourceGroup(g), this.ClientOptions));
        }

        public override ArmResponse<ResourceGroupOperations> Create(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.CreateOrUpdate(name, resourceDetails, cancellationToken);
            return new PhArmResponse<ResourceGroupOperations, ResourceGroup>(
                response,
                g => new ResourceGroupOperations(ClientContext, new PhResourceGroup(g), this.ClientOptions));
        }

        public async override Task<ArmResponse<ResourceGroupOperations>> CreateAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<ResourceGroupOperations, ResourceGroup>(
                response,
                g => new ResourceGroupOperations(ClientContext, new PhResourceGroup(g), this.ClientOptions));
        }

        public override ArmOperation<ResourceGroupOperations> StartCreate(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(
                Operations.CreateOrUpdate(name, resourceDetails, cancellationToken),
                g => new ResourceGroupOperations(ClientContext, new PhResourceGroup(g), this.ClientOptions));
        }

        public async override Task<ArmOperation<ResourceGroupOperations>> StartCreateAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(
                await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false),
                g => new ResourceGroupOperations(ClientContext, new PhResourceGroup(g), this.ClientOptions));
        }

        public Pageable<ResourceGroupOperations> List(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingPageable<ResourceGroup, ResourceGroupOperations>(
                Operations.List(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new PhResourceGroup(s), this.ClientOptions));
        }

        public AsyncPageable<ResourceGroupOperations> ListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingAsyncPageable<ResourceGroup, ResourceGroupOperations>(
                Operations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new PhResourceGroup(s), this.ClientOptions));
        }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;
    }
}
