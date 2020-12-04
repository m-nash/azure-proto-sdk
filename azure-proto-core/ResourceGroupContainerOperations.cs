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
    public class ResourceGroupContainerOperations : ResourceContainerOperations<XResourceGroup, PhResourceGroup>
    {
        public override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        internal ResourceGroupContainerOperations(ArmClientContext context, SubscriptionOperations subscription) : base(context, subscription.Id) { }

        internal ResourceGroupContainerOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public ArmOperation<XResourceGroup> Create(string name, Location location)
        {
            var model = new PhResourceGroup(new ResourceGroup(location));
            return new PhArmOperation<XResourceGroup, ResourceGroup>(
                Operations.CreateOrUpdate(name, model),
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g)));
        }

        public override ArmResponse<XResourceGroup> Create(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.CreateOrUpdate(name, resourceDetails, cancellationToken);
            return new PhArmResponse<XResourceGroup, ResourceGroup>(
                response,
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g)));
        }

        public async override Task<ArmResponse<XResourceGroup>> CreateAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<XResourceGroup, ResourceGroup>(
                response,
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g)));
        }

        public override ArmOperation<XResourceGroup> StartCreate(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XResourceGroup, ResourceGroup>(
                Operations.CreateOrUpdate(name, resourceDetails, cancellationToken),
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g)));
        }

        public async override Task<ArmOperation<XResourceGroup>> StartCreateAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XResourceGroup, ResourceGroup>(
                await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false),
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g)));
        }

        public Pageable<XResourceGroup> List(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingPageable<ResourceGroup, XResourceGroup>(
                Operations.List(null, null, cancellationToken),
                s => new XResourceGroup(ClientContext, new PhResourceGroup(s)));
        }

        public AsyncPageable<XResourceGroup> ListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingAsyncPageable<ResourceGroup, XResourceGroup>(
                Operations.ListAsync(null, null, cancellationToken),
                s => new XResourceGroup(ClientContext, new PhResourceGroup(s)));
        }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;
    }
}
