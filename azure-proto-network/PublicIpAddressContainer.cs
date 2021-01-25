using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Core.Resources;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace azure_proto_network
{
    /// <summary>
    /// A class representing collection of PublicIpAddress and their operations over a resource group.
    /// </summary>
    public class PublicIpAddressContainer : ResourceContainerBase<PublicIpAddress, PublicIPAddressData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicIpAddressContainer"/> class.
        /// </summary>
        /// <param name="genericOperations"> An instance of <see cref="ArmResourceOperations"/> that has an id for a PublicIpAddress. </param>
        internal PublicIpAddressContainer(ArmResourceOperations genericOperations)
            : base(genericOperations.ClientOptions,genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicIpAddressContainer"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resourceGroup"> The ResourceGroup that is the parent of the PublicIpAddress. </param>
        internal PublicIpAddressContainer(AzureResourceManagerClientOptions options, ResourceGroupData resourceGroup)
            : base(options, resourceGroup)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicIpAddressContainer"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="parentId"> The identifier of the ResourceGroup that is the parent of the PublicIpAddress. </param>
        internal PublicIpAddressContainer(AzureResourceManagerClientOptions options, ResourceIdentifier parentId)
            : base(options, parentId)
        {
        }

        /// <summary>
        /// Gets the valid resource type for this resource.
        /// </summary>
        protected override ResourceType ValidResourceType => ResourceGroupOperations.ResourceType;

        private PublicIPAddressesOperations Operations => GetClient((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
            ClientOptions.Convert<NetworkManagementClientOptions>())).PublicIPAddresses;

        /// <inheritdoc />
        public override ArmResponse<PublicIpAddress> Create(string name, PublicIPAddressData resourceDetails)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails);
            return new PhArmResponse<PublicIpAddress, PublicIPAddress>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new PublicIpAddress(ClientOptions, new PublicIPAddressData(n)));
        }

        /// <inheritdoc />
        public override async Task<ArmResponse<PublicIpAddress>> CreateAsync(string name, PublicIPAddressData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<PublicIpAddress, PublicIPAddress>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new PublicIpAddress(ClientOptions, new PublicIPAddressData(n)));
        }

        /// <inheritdoc />
        public override ArmOperation<PublicIpAddress> StartCreate(string name, PublicIPAddressData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PublicIpAddress, PublicIPAddress>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new PublicIpAddress(ClientOptions, new PublicIPAddressData(n)));
        }

        /// <inheritdoc />
        public override async Task<ArmOperation<PublicIpAddress>> StartCreateAsync(string name, PublicIPAddressData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PublicIpAddress, PublicIPAddress>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new PublicIpAddress(ClientOptions, new PublicIPAddressData(n)));
        }

        /// <summary>
        /// Construct an object used to create a public IP address.
        /// </summary>
        /// <param name="location"> The location to create the network security group. </param>
        /// <returns> Object used to create a <see cref="PublicIpAddress"/>. </returns>
        public ArmBuilder<PublicIpAddress, PublicIPAddressData> Construct(LocationData location = null)
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = location ?? DefaultLocation,
            };

            return new ArmBuilder<PublicIpAddress, PublicIPAddressData>(this, new PublicIPAddressData(ipAddress));
        }

        /// <summary>
        /// List the public IP addresses for this resource group.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of <see cref="PublicIPAddress"/> that may take multiple service requests to iterate over. </returns>
        public Pageable<PublicIpAddress> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<PublicIPAddress, PublicIpAddress>(
                Operations.List(Id.Name, cancellationToken),
                this.Convertor());
        }

        /// <summary>
        /// List the public IP addresses for this resource group.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of <see cref="PublicIpAddress"/> that may take multiple service requests to iterate over. </returns>
        public AsyncPageable<PublicIpAddress> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<PublicIPAddress, PublicIpAddress>(
                Operations.ListAsync(Id.Name, cancellationToken),
                this.Convertor());
        }

        /// <summary>
        /// Filters the list of public IP addresses for this resource group represented as generic resources.
        /// </summary>
        /// <param name="filter"> The substring to filter by. </param>
        /// <param name="top"> The number of items to truncate by. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of <see cref="ArmResource"/> that may take multiple service requests to iterate over. </returns>
        public Pageable<ArmResource> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PublicIPAddressData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext(ClientOptions, Id, filters, top, cancellationToken);
        }

        /// <summary>
        /// Filters the list of public IP addresses for this resource group represented as generic resources.
        /// </summary>
        /// <param name="filter"> The substring to filter by. </param>
        /// <param name="top"> The number of items to truncate by. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of <see cref="ArmResource"/> that may take multiple service requests to iterate over. </returns>
        public AsyncPageable<ArmResource> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PublicIPAddressData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync(ClientOptions, Id, filters, top, cancellationToken);
        }

        /// <summary>
        /// Filters the list of public IP addresses for this resource group represented as generic resources.
        /// Makes an additional network call to retrieve the full data model for each network security group.
        /// </summary>
        /// <param name="filter"> The substring to filter by. </param>
        /// <param name="top"> The number of items to truncate by. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of <see cref="PublicIpAddress"/> that may take multiple service requests to iterate over. </returns>
        public Pageable<PublicIpAddress> ListByNameExpanded(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByName(filter, top, cancellationToken);
            return new PhWrappingPageable<ArmResource, PublicIpAddress>(results, s => new PublicIpAddressOperations(s).Get().Value);
        }

        /// <summary>
        /// Filters the list of public IP addresses for this resource group represented as generic resources.
        /// Makes an additional network call to retrieve the full data model for each network security group.
        /// </summary>
        /// <param name="filter"> The substring to filter by. </param>
        /// <param name="top"> The number of items to truncate by. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of <see cref="PublicIpAddress"/> that may take multiple service requests to iterate over. </returns>
        public AsyncPageable<PublicIpAddress> ListByNameExpandedAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByNameAsync(filter, top, cancellationToken);
            return new PhWrappingAsyncPageable<ArmResource, PublicIpAddress>(results, s => new PublicIpAddressOperations(s).Get().Value);
        }

        private Func<PublicIPAddress, PublicIpAddress> Convertor()
        {
            return s => new PublicIpAddress(ClientOptions, new PublicIPAddressData(s));
        }
    }
}
