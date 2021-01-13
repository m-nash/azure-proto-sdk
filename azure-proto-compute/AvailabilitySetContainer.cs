using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Core.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// A class representing collection of availability set and their operations over a resource group.
    /// </summary>
    public class AvailabilitySetContainer : ResourceContainerBase<AvailabilitySet, AvailabilitySetData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilitySetContainer"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resourceGroup"> The data of Resource Group. </param>
        internal AvailabilitySetContainer(AzureResourceManagerClientOptions options, ResourceGroupData resourceGroup)
            : base(options, resourceGroup)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilitySetContainer"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="parentId"> The resource Id of the parent resource. </param>
        internal AvailabilitySetContainer(AzureResourceManagerClientOptions options, ResourceIdentifier parentId)
            : base(options, parentId)
        {
        }

        /// <inheritdoc/>
        protected override ResourceType ValidResourceType => ResourceGroupOperations.ResourceType;

        /// <inheritdoc/>
        public override ArmResponse<AvailabilitySet> Create(string name, AvailabilitySetData resourceDetails)
        {
            var response = Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model);
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                response,
                a => new AvailabilitySet(ClientOptions, new AvailabilitySetData(a)));
        }

        /// <inheritdoc/>
        public async override Task<ArmResponse<AvailabilitySet>> CreateAsync(string name, AvailabilitySetData resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                response,
                a => new AvailabilitySet(ClientOptions, new AvailabilitySetData(a)));
        }

        /// <inheritdoc/>
        public override ArmOperation<AvailabilitySet> StartCreate(string name, AvailabilitySetData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                a => new AvailabilitySet(ClientOptions, new AvailabilitySetData(a)));
        }

        /// <inheritdoc/>
        public async override Task<ArmOperation<AvailabilitySet>> StartCreateAsync(string name, AvailabilitySetData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                a => new AvailabilitySet(ClientOptions, new AvailabilitySetData(a)));
        }

        /// <summary>
        /// Constructs an object used to create an availability set.
        /// </summary>
        /// <param name="skuName"> The sku name of the resource. </param>
        /// <param name="location"> The location of the resource. </param>
        /// <returns> A builder with <see cref="AvailabilitySet"> and <see cref="AvailabilitySetData"/>. </returns>
        public ArmBuilder<AvailabilitySet, AvailabilitySetData> Construct(string skuName, Location location = null)
        {
            var availabilitySet = new Azure.ResourceManager.Compute.Models.AvailabilitySet(location ?? DefaultLocation)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName }
            };

            return new ArmBuilder<AvailabilitySet, AvailabilitySetData>(this, new AvailabilitySetData(availabilitySet));
        }

        /// <summary>
        /// Filters the list of availabitlity set for this resource group represented as generic resources.
        /// </summary>
        /// <param name="filter"> The filter used in this operation. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of resource that may take multiple service requests to iterate over. </returns>
        public Pageable<ArmResource> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(AvailabilitySetData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext(ClientOptions, Id, filters, top, cancellationToken);
        }

        /// <summary>
        /// Filters the list of availabitlity set for this resource group represented as generic resources.
        /// </summary>
        /// <param name="filter"> The filter used in this operation. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of resource that may take multiple service requests to iterate over. </returns>
        public AsyncPageable<ArmResource> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(AvailabilitySetData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync(ClientOptions, Id, filters, top, cancellationToken);
        }

        /// <summary>
        /// Filters the list of availabitlity set for this resource group.
        /// Makes an additional network call to retrieve the full data model for each resource group.
        /// </summary>
        /// <param name="filter"> The filter used in this operation. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of availability set that may take multiple service requests to iterate over. </returns>
        public Pageable<AvailabilitySet> ListByNameExpanded(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByName(filter, top, cancellationToken);
            return new PhWrappingPageable<ArmResource, AvailabilitySet>(results, s => new AvailabilitySetOperations(s).Get().Value);
        }

        /// <summary>
        /// Filters the list of availabitlity set for this resource group.
        /// Makes an additional network call to retrieve the full data model for each resource group.
        /// </summary>
        /// <param name="filter"> The filter used in this operation. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An asyc collection of availability set that may take multiple service requests to iterate over. </returns>
        public AsyncPageable<AvailabilitySet> ListByNameExpandedAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByNameAsync(filter, top, cancellationToken);
            return new PhWrappingAsyncPageable<ArmResource, AvailabilitySet>(results, s => new AvailabilitySetOperations(s).Get().Value);
        }

        private AvailabilitySetsOperations Operations => GetClient((uri, cred) => new ComputeManagementClient(uri, Id.Subscription, cred,
                    ClientOptions.Convert<ComputeManagementClientOptions>())).AvailabilitySets;
    }
}
