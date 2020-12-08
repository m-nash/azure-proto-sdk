using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    public class AvailabilitySetOperations : ResourceOperationsBase<XAvailabilitySet, PhAvailabilitySet>, ITaggable<XAvailabilitySet, PhAvailabilitySet>, IDeletableResource<XAvailabilitySet, PhAvailabilitySet>
    {
        public AvailabilitySetOperations(ArmClientContext context, TrackedResource resource, ArmClientOptions clientOptions) : base(context, resource, clientOptions) { }

        public AvailabilitySetOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions) : base(context, id, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.Delete(Id.ResourceGroup, Id.Name));
        }

        public async  Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.DeleteAsync(Id.ResourceGroup, Id.Name));
        }

        public override ArmResponse<XAvailabilitySet> Get()
        {
            return new PhArmResponse<XAvailabilitySet, AvailabilitySet>(Operations.Get(Id.ResourceGroup, Id.Name), a => { Resource = new PhAvailabilitySet(a); return new XAvailabilitySet(ClientContext, Resource as PhAvailabilitySet, ClientOptions); });
        }

        public async override Task<ArmResponse<XAvailabilitySet>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<XAvailabilitySet, AvailabilitySet>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, cancellationToken), a => { Resource = new PhAvailabilitySet(a); return new XAvailabilitySet(ClientContext, Resource as PhAvailabilitySet, ClientOptions); });
        }

        public ArmOperation<XAvailabilitySet> Update(AvailabilitySetUpdate patchable)
        {
            return new PhArmOperation<XAvailabilitySet, AvailabilitySet>(Operations.Update(Id.ResourceGroup, Id.Name, patchable), a => { Resource = new PhAvailabilitySet(a); return new XAvailabilitySet(ClientContext, Resource as PhAvailabilitySet, ClientOptions); });
        }

        public async Task<ArmOperation<XAvailabilitySet>> UpdateAsync(AvailabilitySetUpdate patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XAvailabilitySet, AvailabilitySet>(await Operations.UpdateAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken), a => { Resource = new PhAvailabilitySet(a); return new XAvailabilitySet(ClientContext, Resource as PhAvailabilitySet, ClientOptions); });
        }

        public ArmOperation<XAvailabilitySet> AddTag(string key, string value)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return Update(patchable);
        }

        public Task<ArmOperation<XAvailabilitySet>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return UpdateAsync(patchable);
        }

        internal AvailabilitySetsOperations Operations => GetClient<ComputeManagementClient>((uri, cred) => new ComputeManagementClient(uri, Id.Subscription, cred, 
                    ArmClientOptions.convert<ComputeManagementClientOptions>(ClientOptions))).AvailabilitySets;
    }
}
