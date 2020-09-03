using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    public class AvailabilitySetOperations : ResourceClientBase<PhAvailabilitySet>
    {

        public AvailabilitySetOperations(ArmClientBase parent, TrackedResource context) : base(parent, context)
        {
        }
        public AvailabilitySetOperations(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.Delete(Context.ResourceGroup, Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.DeleteAsync(Context.ResourceGroup, Context.Name));
        }

        public override Response<ResourceClientBase<PhAvailabilitySet>> Get()
        {
            return new PhArmResponse<ResourceClientBase<PhAvailabilitySet>, AvailabilitySet>(Operations.Get(Context.ResourceGroup, Context.Name), a => { Resource = new PhAvailabilitySet(a); return this; });
        }

        public async override Task<Response<ResourceClientBase<PhAvailabilitySet>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceClientBase<PhAvailabilitySet>, AvailabilitySet>(await Operations.GetAsync(Context.ResourceGroup, Context.Name, cancellationToken), a => { Resource = new PhAvailabilitySet(a); return this; });
        }

        public ArmOperation<ResourceClientBase<PhAvailabilitySet>> Update(AvailabilitySetUpdate patchable)
        {
            return new PhArmOperation<ResourceClientBase<PhAvailabilitySet>, AvailabilitySet>(Operations.Update(Context.ResourceGroup, Context.Name, patchable), a => { Resource = new PhAvailabilitySet(a); return this; });
        }

        public async Task<ArmOperation<ResourceClientBase<PhAvailabilitySet>>> UpdateAsync(AvailabilitySetUpdate patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceClientBase<PhAvailabilitySet>, AvailabilitySet>(await Operations.UpdateAsync(Context.ResourceGroup, Context.Name, patchable, cancellationToken), a => { Resource = new PhAvailabilitySet(a); return this; });
        }

        public override ArmOperation<ResourceClientBase<PhAvailabilitySet>> AddTag(string key, string value)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return Update(patchable);
        }

        public override Task<ArmOperation<ResourceClientBase<PhAvailabilitySet>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return UpdateAsync(patchable);
        }

        internal AvailabilitySetsOperations Operations => GetClient<ComputeManagementClient>((uri, cred) => new ComputeManagementClient(uri, Context.Subscription, cred)).AvailabilitySets;
    }
}
