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

        internal ResourceGroupContainerOperations(ArmClientContext other, ResourceIdentifier context) : base(other, context)
        {
        }
        internal ResourceGroupContainerOperations(ArmClientContext other, Resource context) : base(other, context)
        {
        }

        internal ResourceGroupContainerOperations(OperationsBase other, ResourceIdentifier context) : base(other, context)
        {
        }
        internal ResourceGroupContainerOperations(OperationsBase other, Resource context) : base(other, context)
        {
        }

        public ArmOperation<ResourceGroupOperations> Create(string name, Location location)
        {
            var model = new PhResourceGroup(new ResourceGroup(location));
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(
                Operations.CreateOrUpdate(name, model),
                g => new ResourceGroupOperations(this, new PhResourceGroup(g)));
        }

        public override ArmResponse<ResourceGroupOperations> Create(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.CreateOrUpdate(name, resourceDetails, cancellationToken);
            return new PhArmResponse<ResourceGroupOperations, ResourceGroup>(
                response,
                g => new ResourceGroupOperations(this, new PhResourceGroup(g)));
        }

        public async override Task<ArmResponse<ResourceGroupOperations>> CreateAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<ResourceGroupOperations, ResourceGroup>(
                response,
                g => new ResourceGroupOperations(this, new PhResourceGroup(g)));
        }

        public override ArmOperation<ResourceGroupOperations> StartCreate(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(
                Operations.CreateOrUpdate(name, resourceDetails, cancellationToken),
                g => new ResourceGroupOperations(this, new PhResourceGroup(g)));
        }

        public async override Task<ArmOperation<ResourceGroupOperations>> StartCreateAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(
                await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false),
                g => new ResourceGroupOperations(this, new PhResourceGroup(g)));
        }

        public Pageable<ResourceOperationsBase<PhResourceGroup>> ListResourceGroups(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingPageable<ResourceGroup, ResourceOperationsBase<PhResourceGroup>>(
                Operations.List(null, null, cancellationToken),
                s => new ResourceGroupOperations(this, new PhResourceGroup(s)));
        }

        public AsyncPageable<ResourceOperationsBase<PhResourceGroup>> ListResourceGroupsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingAsyncPageable<ResourceGroup, ResourceOperationsBase<PhResourceGroup>>(
                Operations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroupOperations(this, new PhResourceGroup(s)));
        }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;
    }
}
