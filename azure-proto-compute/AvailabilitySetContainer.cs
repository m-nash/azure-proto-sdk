using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Operatiosn class for Availability Set Contaienrs (resource groups)
    /// </summary>
    public class AvailabilitySetContainer : ResourceContainerOperations<AvailabilitySet, AvailabilitySetData>
    {
        public AvailabilitySetContainer(ArmClientContext context, ResourceGroupData resourceGroup, ArmClientOptions clientOptions) : base(context, resourceGroup, clientOptions) { }

        internal AvailabilitySetContainer(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions):base(context, id, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public override ArmResponse<AvailabilitySet> Create(string name, AvailabilitySetData resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                response,
                a => new AvailabilitySet(ClientContext, new AvailabilitySetData(a), ClientOptions));
        }

        public async override Task<ArmResponse<AvailabilitySet>> CreateAsync(string name, AvailabilitySetData resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                response,
                a => new AvailabilitySet(ClientContext, new AvailabilitySetData(a), ClientOptions));
        }

        public override ArmOperation<AvailabilitySet> StartCreate(string name, AvailabilitySetData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                a => new AvailabilitySet(ClientContext, new AvailabilitySetData(a), ClientOptions));
        }

        public async override Task<ArmOperation<AvailabilitySet>> StartCreateAsync(string name, AvailabilitySetData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                a => new AvailabilitySet(ClientContext, new AvailabilitySetData(a), ClientOptions));
        }

        public ArmBuilder<AvailabilitySet, AvailabilitySetData> Construct(string skuName, Location location = null)
        {
            var availabilitySet = new Azure.ResourceManager.Compute.Models.AvailabilitySet(location ?? base.DefaultLocation)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName }
            };

            return new ArmBuilder<AvailabilitySet, AvailabilitySetData>(this, new AvailabilitySetData(availabilitySet));
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(AvailabilitySetData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(AvailabilitySetData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public Pageable<AvailabilitySet> ListByNameExpanded(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByName(filter, top, cancellationToken);
            return new PhWrappingPageable<ArmResourceOperations, AvailabilitySet>(results, s => new AvailabilitySetOperations(s).Get().Value);
        }

        public AsyncPageable<AvailabilitySet> ListByNameExpandedAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByNameAsync(filter, top, cancellationToken);
            return new PhWrappingAsyncPageable<ArmResourceOperations, AvailabilitySet>(results, s => new AvailabilitySetOperations(s).Get().Value);
        }

        internal AvailabilitySetsOperations Operations => GetClient((uri, cred) => new ComputeManagementClient(uri, Id.Subscription, cred, 
                    ArmClientOptions.Convert<ComputeManagementClientOptions>(ClientOptions))).AvailabilitySets;
    }
}
