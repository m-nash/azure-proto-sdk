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
    public class ResourceGroupContainerOperations : ResourceContainerOperations<PhResourceGroup>
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
        
        public override ArmOperation<ResourceOperationsBase<PhResourceGroup>> Create(string name, PhResourceGroup resourceDetails)
        {
            return new PhArmOperation<ResourceOperationsBase<PhResourceGroup>, ResourceGroup>(Operations.CreateOrUpdate(name, resourceDetails), g => ResourceGroup(new PhResourceGroup(g)));
        }

        public ArmOperation<ResourceOperationsBase<PhResourceGroup>> Create(string name, Location location)
        {
            var model = new PhResourceGroup(new ResourceGroup(location));
            return new PhArmOperation<ResourceOperationsBase<PhResourceGroup>, ResourceGroup>(Operations.CreateOrUpdate(name, model), g => ResourceGroup(new PhResourceGroup(g)));
        }


        public async override Task<ArmOperation<ResourceOperationsBase<PhResourceGroup>>> CreateAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhResourceGroup>, ResourceGroup>(await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken), g => ResourceGroup(new PhResourceGroup(g)));
        }

        public Pageable<ResourceOperationsBase<PhResourceGroup>> ListResourceGroups(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingPageable<ResourceGroup, ResourceOperationsBase<PhResourceGroup>>(Operations.List(null, null, cancellationToken), s => ResourceGroup(new PhResourceGroup(s)));
        }

        public AsyncPageable<ResourceOperationsBase<PhResourceGroup>> ListResourceGroupsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingAsyncPageable<ResourceGroup, ResourceOperationsBase<PhResourceGroup>>(Operations.ListAsync(null, null, cancellationToken), s => ResourceGroup(new PhResourceGroup(s)));
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier context)
        {
            return new ResourceGroupOperations(this, context);
        }

        public ResourceGroupOperations ResourceGroup(Resource context)
        {
            return new ResourceGroupOperations(this, context);
        }

        public ResourceGroupOperations ResourceGroup(string rg)
        {
            return new ResourceGroupOperations(this, $"{Id}/resourceGroups/{rg}");
        }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;
    }
}
