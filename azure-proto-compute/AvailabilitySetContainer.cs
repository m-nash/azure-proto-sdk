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
    public class AvailabilitySetContainer : ResourceContainerOperations<AvailabilitySetOperations, PhAvailabilitySet>
    {
        public AvailabilitySetContainer(ArmClientContext context, PhResourceGroup resourceGroup, ArmClientOptions clientOptions) : base(context, resourceGroup, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public override ArmResponse<AvailabilitySetOperations> Create(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<AvailabilitySetOperations, AvailabilitySet>(
                response,
                a => new AvailabilitySetOperations(ClientContext, new PhAvailabilitySet(a), this.ClientOptions));
        }

        public async override Task<ArmResponse<AvailabilitySetOperations>> CreateAsync(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<AvailabilitySetOperations, AvailabilitySet>(
                response,
                a => new AvailabilitySetOperations(ClientContext, new PhAvailabilitySet(a), this.ClientOptions));
        }

        public override ArmOperation<AvailabilitySetOperations> StartCreate(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<AvailabilitySetOperations, AvailabilitySet>(
                Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                a => new AvailabilitySetOperations(ClientContext, new PhAvailabilitySet(a), this.ClientOptions));
        }

        public async override Task<ArmOperation<AvailabilitySetOperations>> StartCreateAsync(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<AvailabilitySetOperations, AvailabilitySet>(
                await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                a => new AvailabilitySetOperations(ClientContext, new PhAvailabilitySet(a), this.ClientOptions));
        }

        public ArmBuilder<AvailabilitySetOperations, PhAvailabilitySet> Construct(string skuName, Location location = null)
        {
            var availabilitySet = new AvailabilitySet(location ?? DefaultLocation)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName }
            };

            return new ArmBuilder<AvailabilitySetOperations, PhAvailabilitySet>(this, new PhAvailabilitySet(availabilitySet));
        }

        internal AvailabilitySetsOperations Operations => GetClient((uri, cred) => new ComputeManagementClient(uri, Id.Subscription, cred, 
                    ArmClientOptions.convert<ComputeManagementClientOptions>(this.ClientOptions))).AvailabilitySets;
    }
}
