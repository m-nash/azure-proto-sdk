using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    public class AvailabilitySetOperations : ResourceOperationsBase<PhAvailabilitySet>
    {

        public AvailabilitySetOperations(ArmClientContext parent, TrackedResource context) : base(parent, context)
        {
        }
        public AvailabilitySetOperations(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }
        public AvailabilitySetOperations(OperationsBase parent, TrackedResource context) : base(parent, context)
        {
        }
        public AvailabilitySetOperations(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }
        public override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.Delete(Context.ResourceGroup, Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.DeleteAsync(Context.ResourceGroup, Context.Name));
        }

        public override Response<ResourceOperationsBase<PhAvailabilitySet>> Get()
        {
            return new PhArmResponse<ResourceOperationsBase<PhAvailabilitySet>, AvailabilitySet>(Operations.Get(Context.ResourceGroup, Context.Name), a => { Resource = new PhAvailabilitySet(a); return this; });
        }

        public async override Task<Response<ResourceOperationsBase<PhAvailabilitySet>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperationsBase<PhAvailabilitySet>, AvailabilitySet>(await Operations.GetAsync(Context.ResourceGroup, Context.Name, cancellationToken), a => { Resource = new PhAvailabilitySet(a); return this; });
        }

        public ArmOperation<ResourceOperationsBase<PhAvailabilitySet>> Update(AvailabilitySetUpdate patchable)
        {
            return new PhArmOperation<ResourceOperationsBase<PhAvailabilitySet>, AvailabilitySet>(Operations.Update(Context.ResourceGroup, Context.Name, patchable), a => { Resource = new PhAvailabilitySet(a); return this; });
        }

        public async Task<ArmOperation<ResourceOperationsBase<PhAvailabilitySet>>> UpdateAsync(AvailabilitySetUpdate patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhAvailabilitySet>, AvailabilitySet>(await Operations.UpdateAsync(Context.ResourceGroup, Context.Name, patchable, cancellationToken), a => { Resource = new PhAvailabilitySet(a); return this; });
        }

        public override ArmOperation<ResourceOperationsBase<PhAvailabilitySet>> AddTag(string key, string value)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return Update(patchable);
        }

        public override Task<ArmOperation<ResourceOperationsBase<PhAvailabilitySet>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return UpdateAsync(patchable);
        }

        internal AvailabilitySetsOperations Operations => GetClient<ComputeManagementClient>((uri, cred) => new ComputeManagementClient(uri, Context.Subscription, cred)).AvailabilitySets;
    }
}
