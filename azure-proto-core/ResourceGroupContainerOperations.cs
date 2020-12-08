namespace azure_proto_core
{
    using System.Threading;
    using System.Threading.Tasks;
    using Azure;
    using Azure.ResourceManager.Resources;
    using azure_proto_core.Adapters;

    /// <summary>
    /// Operations for the RespourceGroups container in the given subscription context.  Allows Creating and listign respource groups
    /// and provides an attachment point for Collections of Tracked Resources.
    /// </summary>
    public class ResourceGroupContainerOperations : ResourceContainerOperations<ResourceGroup, ResourceGroupData>
    {
        public override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        internal ResourceGroupContainerOperations(ArmClientContext context, SubscriptionOperations subscription)
            : base(context, subscription.Id) { }

        internal ResourceGroupContainerOperations(ArmClientContext context, ResourceIdentifier id)
            : base(context, id) { }

        public ArmOperation<ResourceGroup> Create(string name, Location location)
        {
            var model = new ResourceGroupData(new Azure.ResourceManager.Resources.Models.ResourceGroup(location));
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                Operations.CreateOrUpdate(name, model),
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g)));
        }

        public override ArmResponse<ResourceGroup> Create(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.CreateOrUpdate(name, resourceDetails, cancellationToken);
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                response,
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g)));
        }

        public async override Task<ArmResponse<ResourceGroup>> CreateAsync(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                response,
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g)));
        }

        public override ArmOperation<ResourceGroup> StartCreate(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                Operations.CreateOrUpdate(name, resourceDetails, cancellationToken),
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g)));
        }

        public async override Task<ArmOperation<ResourceGroup>> StartCreateAsync(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false),
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g)));
        }

        public Pageable<ResourceGroup> List(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroup>(
                Operations.List(null, null, cancellationToken),
                s => new ResourceGroup(ClientContext, new ResourceGroupData(s)));
        }

        public AsyncPageable<ResourceGroup> ListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroup>(
                Operations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroup(ClientContext, new ResourceGroupData(s)));
        }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;
    }
}
