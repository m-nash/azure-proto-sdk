using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    public class AvailabilitySetOperations : ResourceOperations<PhAvailabilitySet, AvailabilitySetUpdate, Response<PhAvailabilitySet>, Response>
    {

        public AvailabilitySetOperations(ArmOperations parent, TrackedResource context) : base(parent, context)
        {
        }
        public AvailabilitySetOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public override Response Delete()
        {
            return Operations.Delete(Context.ResourceGroup, Context.Name);
        }

        public override Task<Response> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return Operations.DeleteAsync(Context.ResourceGroup, Context.Name);
        }

        public override Response<PhAvailabilitySet> Get()
        {
            return new PhResponse<PhAvailabilitySet, AvailabilitySet>(Operations.Get(Context.ResourceGroup, Context.Name), a => new PhAvailabilitySet(a));
        }

        public async override Task<Response<PhAvailabilitySet>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhAvailabilitySet, AvailabilitySet>(await Operations.GetAsync(Context.ResourceGroup, Context.Name, cancellationToken), a => new PhAvailabilitySet(a));
        }

        public override Response<PhAvailabilitySet> Update(AvailabilitySetUpdate patchable)
        {
            return new PhResponse<PhAvailabilitySet, AvailabilitySet>(Operations.Update(Context.ResourceGroup, Context.Name, patchable), a => new PhAvailabilitySet(a));
        }

        public async override Task<Response<PhAvailabilitySet>> UpdateAsync(AvailabilitySetUpdate patchable, CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhAvailabilitySet, AvailabilitySet>(await Operations.UpdateAsync(Context.ResourceGroup, Context.Name, patchable), a => new PhAvailabilitySet(a));
        }

        internal AvailabilitySetsOperations Operations => GetClient<ComputeManagementClient>((uri, cred) => new ComputeManagementClient(uri, Context.Subscription, cred)).AvailabilitySets;
    }
}
