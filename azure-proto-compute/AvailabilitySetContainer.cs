using Azure;
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
    public class AvailabilitySetContainer : ArmResourceContainerOperations<PhAvailabilitySet, Response<PhAvailabilitySet>>
    {
        public AvailabilitySetContainer(ArmOperations parent, TrackedResource context):base(parent, context) 
        {
        }
        public AvailabilitySetContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public override Response<PhAvailabilitySet> Create(string name, PhAvailabilitySet resourceDetails)
        {
            return new PhResponse<PhAvailabilitySet, AvailabilitySet>(Operations.CreateOrUpdate(Context.ResourceGroup, name, resourceDetails.Model), a => new PhAvailabilitySet(a));
        }

        public async override Task<Response<PhAvailabilitySet>> CreateAsync(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhAvailabilitySet, AvailabilitySet>(await Operations.CreateOrUpdateAsync(Context.ResourceGroup, name, resourceDetails.Model, cancellationToken), a => new PhAvailabilitySet(a));
        }

        public PhAvailabilitySet ConstructAvailabilitySet(string skuName, Location location = null)
        {
            var availabilitySet = new AvailabilitySet(location ?? DefaultLocation)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName },
            };

            return new PhAvailabilitySet(availabilitySet);
        }

        public AvailabilitySetOperations AvailabilitySet(string name)
        {
            return new AvailabilitySetOperations(this, new ResourceIdentifier($"{Context}/providers/Microsoft.Compute/availabilitySets/{name}"));
        }

        public AvailabilitySetOperations AvailabilitySet(ResourceIdentifier vm)
        {
            return new AvailabilitySetOperations(this, vm);
        }

        public AvailabilitySetOperations AvailabilitySet(TrackedResource vm)
        {
            return new AvailabilitySetOperations(this, vm);
        }


        internal AvailabilitySetsOperations Operations => GetClient<ComputeManagementClient>((uri, cred) => new ComputeManagementClient(uri, Context.Subscription, cred)).AvailabilitySets;
    }
}
