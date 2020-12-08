using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    public class AvailabilitySetOperations : ResourceOperationsBase<AvailabilitySet, AvailabilitySetData>, ITaggable<AvailabilitySet, AvailabilitySetData>, IDeletableResource<AvailabilitySet, AvailabilitySetData>
    {
        public AvailabilitySetOperations(ArmResourceOperations genericOperations) : base(genericOperations) { }

        public AvailabilitySetOperations(ArmClientContext context, TrackedResource resource) : base(context, resource) { }

        public AvailabilitySetOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.Delete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.DeleteAsync(Id.ResourceGroup, Id.Name));
        }

        public override ArmResponse<AvailabilitySet> Get()
        {
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(Operations.Get(base.Id.ResourceGroup, base.Id.Name), a => { base.Resource = new AvailabilitySetData(a); return new AvailabilitySet(base.ClientContext, base.Resource as AvailabilitySetData); });
        }

        public async override Task<ArmResponse<AvailabilitySet>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(await Operations.GetAsync(base.Id.ResourceGroup, base.Id.Name, cancellationToken), a => { base.Resource = new AvailabilitySetData(a); return new AvailabilitySet(base.ClientContext, base.Resource as AvailabilitySetData); });
        }

        public ArmOperation<AvailabilitySet> Update(AvailabilitySetUpdate patchable)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(Operations.Update(base.Id.ResourceGroup, base.Id.Name, patchable), a => { base.Resource = new AvailabilitySetData(a); return new AvailabilitySet(base.ClientContext, base.Resource as AvailabilitySetData); });
        }

        public async Task<ArmOperation<AvailabilitySet>> UpdateAsync(AvailabilitySetUpdate patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(await Operations.UpdateAsync(base.Id.ResourceGroup, base.Id.Name, patchable, cancellationToken), a => { base.Resource = new AvailabilitySetData(a); return new AvailabilitySet(base.ClientContext, base.Resource as AvailabilitySetData); });
        }

        public ArmOperation<AvailabilitySet> AddTag(string key, string value)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return Update(patchable);
        }

        public Task<ArmOperation<AvailabilitySet>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return UpdateAsync(patchable);
        }

        internal AvailabilitySetsOperations Operations => GetClient<ComputeManagementClient>((uri, cred) => new ComputeManagementClient(uri, Id.Subscription, cred)).AvailabilitySets;
    }
}
