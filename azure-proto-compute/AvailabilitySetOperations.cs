using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    public class AvailabilitySetOperations : ResourceOperationsBase<AvailabilitySet>, ITaggable<AvailabilitySet>, IDeletable
    {
        internal AvailabilitySetOperations(ArmResourceOperations genericOperations)
            : base(genericOperations)
        {
        }

        internal AvailabilitySetOperations(ArmClientContext context, TrackedResource resource, ArmClientOptions clientOptions)
            : base(context, resource, clientOptions)
        {
        }

        internal AvailabilitySetOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : base(context, id, clientOptions)
        {
        }

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
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                Operations.Get(Id.ResourceGroup, Id.Name),
                a =>
                {
                    Resource = new AvailabilitySetData(a);
                    return new AvailabilitySet(ClientContext, Resource as AvailabilitySetData, ClientOptions);
                });
        }

        public async override Task<ArmResponse<AvailabilitySet>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                await Operations.GetAsync(Id.ResourceGroup, Id.Name, cancellationToken),
                a =>
                {
                    Resource = new AvailabilitySetData(a);
                    return new AvailabilitySet(ClientContext, Resource as AvailabilitySetData, ClientOptions);
                });
        }

        public ArmOperation<AvailabilitySet> Update(AvailabilitySetUpdate patchable)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                Operations.Update(Id.ResourceGroup, Id.Name, patchable),
                a =>
                {
                    Resource = new AvailabilitySetData(a);
                    return new AvailabilitySet(ClientContext, Resource as AvailabilitySetData, ClientOptions);
                });
        }

        public async Task<ArmOperation<AvailabilitySet>> UpdateAsync(AvailabilitySetUpdate patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                await Operations.UpdateAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                a =>
                {
                    Resource = new AvailabilitySetData(a);
                    return new AvailabilitySet(ClientContext, Resource as AvailabilitySetData, ClientOptions);
                });
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

        internal AvailabilitySetsOperations Operations => GetClient<ComputeManagementClient>((uri, cred) => new ComputeManagementClient(uri, Id.Subscription, cred, 
                    ArmClientOptions.Convert<ComputeManagementClientOptions>(ClientOptions))).AvailabilitySets;
    }
}
