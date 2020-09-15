using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Operatiosn class for Availability Set Contaienrs (resource groups)
    /// </summary>
    public class AvailabilitySetContainer : ResourceContainerOperations<PhAvailabilitySet>
    {
        public AvailabilitySetContainer(ArmClientContext parent, TrackedResource context):base(parent, context) 
        {
        }
        public AvailabilitySetContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public AvailabilitySetContainer(OperationsBase parent, TrackedResource context) : base(parent, context)
        {
        }
        public AvailabilitySetContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public AvailabilitySetOperations AvailabilitySet(string name)
        {
            return new AvailabilitySetOperations(this, new ResourceIdentifier($"{Id}/providers/Microsoft.Compute/availabilitySets/{name}"));
        }

        public AvailabilitySetOperations AvailabilitySet(ResourceIdentifier vm)
        {
            return new AvailabilitySetOperations(this, vm);
        }

        public AvailabilitySetOperations AvailabilitySet(TrackedResource vm)
        {
            return new AvailabilitySetOperations(this, vm);
        }

        public override ArmOperation<ResourceOperationsBase<PhAvailabilitySet>> Create(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmOperation<ResourceOperationsBase<PhAvailabilitySet>, AvailabilitySet>(
                operation,
                a => AvailabilitySet(new PhAvailabilitySet(a)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhAvailabilitySet>>> CreateAsync(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmOperation<ResourceOperationsBase<PhAvailabilitySet>, AvailabilitySet>(
                operation,
                a => AvailabilitySet(new PhAvailabilitySet(a)));
        }

        public override ArmOperation<ResourceOperationsBase<PhAvailabilitySet>> StartCreate(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhAvailabilitySet>, AvailabilitySet>(
                Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                a => AvailabilitySet(new PhAvailabilitySet(a)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhAvailabilitySet>>> StartCreateAsync(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhAvailabilitySet>, AvailabilitySet>(
                await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                a => AvailabilitySet(new PhAvailabilitySet(a)));
        }

        internal AvailabilitySetsOperations Operations => GetClient<ComputeManagementClient>((uri, cred) => new ComputeManagementClient(uri, Id.Subscription, cred)).AvailabilitySets;
    }
}
