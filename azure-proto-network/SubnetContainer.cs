using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Adapters;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace azure_proto_network
{
    public class SubnetContainer : ResourceContainerBase<Subnet, SubnetData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubnetContainer"/> class.
        /// </summary>
        /// <param name="virtualNetwork"> The virtual network associate with this subnet </param>
        internal SubnetContainer(VirtualNetworkOperations virtualNetwork)
            : base(virtualNetwork)
        {
        }

        /// <inheritdoc/>
        protected override ResourceType ValidResourceType => VirtualNetworkOperations.ResourceType;

        private SubnetsOperations Operations => new NetworkManagementClient(
            Id.Subscription,
            BaseUri,
            Credential,
            ClientOptions.Convert<NetworkManagementClientOptions>()).Subnets;

        /// <inheritdoc/>
        public override ArmResponse<Subnet> Create(string name, SubnetData resourceDetails)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, name, resourceDetails.Model);
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                s => new Subnet(Parent, new SubnetData(s, Location.Default)));
        }

        /// <inheritdoc/>
        public override async Task<ArmResponse<Subnet>> CreateAsync(string name, SubnetData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                s => new Subnet(Parent, new SubnetData(s, Location.Default)));
        }

        /// <inheritdoc/>
        public override ArmOperation<Subnet> StartCreate(string name, SubnetData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<Subnet, Azure.ResourceManager.Network.Models.Subnet>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken),
                s => new Subnet(Parent, new SubnetData(s, Location.Default)));
        }

        /// <inheritdoc/>
        public async override Task<ArmOperation<Subnet>> StartCreateAsync(string name, SubnetData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<Subnet, Azure.ResourceManager.Network.Models.Subnet>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                s => new Subnet(Parent, new SubnetData(s, Location.Default)));
        }

        /// <summary>
        /// Constructs an object used to create a subnet.
        /// </summary>
        /// <param name="name"> The name of the subnet </param>
        /// <param name="subnetCidr"> The CIDR of the resource. </param>
        /// <param name="location"> The location of the resource. </param>
        /// <param name="group"> The network security group of the resource. </param>
        /// <returns> A builder with <see cref="Subnet"> and <see cref="Subnet"/>. </returns>
        public ArmBuilder<Subnet, SubnetData> Construct(string name, string subnetCidr, Location location = null, NetworkSecurityGroupData group = null)
        {
            var subnet = new Azure.ResourceManager.Network.Models.Subnet()
            {
                Name = name,
                AddressPrefix = subnetCidr,
            };

            if (null != group)
            {
                subnet.NetworkSecurityGroup = group.Model;
            }

            return new ArmBuilder<Subnet, SubnetData>(this, new SubnetData(subnet, location ?? DefaultLocation));
        }
        
        /// <summary>
        /// Lists the subnets for this virtual network.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of resource operations that may take multiple service requests to iterate over. </returns>
        public Pageable<SubnetOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.Subnet, SubnetOperations>(
                Operations.List(Id.ResourceGroup, Id.Name, cancellationToken),
                this.convertor());
        }

        /// <summary>
        /// Lists the subnets for this virtual network.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of resource operations that may take multiple service requests to iterate over. </returns>
        public AsyncPageable<SubnetOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.Subnet, SubnetOperations>(
                Operations.ListAsync(Id.ResourceGroup, Id.Name, cancellationToken),
                this.convertor());
        }

        private Func<Azure.ResourceManager.Network.Models.Subnet, Subnet> convertor()
        {
            //TODO: Subnet will be a proxy resource and not a tracked resource ADO #4481
            return s => new Subnet(Parent, new SubnetData(s, Location.Default));
        }
    }
}
