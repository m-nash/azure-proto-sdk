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
        internal SubnetContainer(VirtualNetworkOperations virtualNetwork)
            : base(virtualNetwork)
        {
        }

        protected override ResourceType ValidResourceType => VirtualNetworkOperations.ResourceType;

        internal SubnetsOperations Operations => new NetworkManagementClient(
            Id.Subscription,
            BaseUri,
            Credential,
            ClientOptions.Convert<NetworkManagementClientOptions>()).Subnets;

        public override ArmResponse<Subnet> Create(string name, SubnetData resourceDetails)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, name, resourceDetails.Model);
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                s => new Subnet(Parent, new SubnetData(s, Location.Default)));
        }

        public async override Task<ArmResponse<Subnet>> CreateAsync(string name, SubnetData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                s => new Subnet(Parent, new SubnetData(s, Location.Default)));
        }

        public override ArmOperation<Subnet> StartCreate(string name, SubnetData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<Subnet, Azure.ResourceManager.Network.Models.Subnet>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken),
                s => new Subnet(Parent, new SubnetData(s, Location.Default)));
        }

        public async override Task<ArmOperation<Subnet>> StartCreateAsync(string name, SubnetData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<Subnet, Azure.ResourceManager.Network.Models.Subnet>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                s => new Subnet(Parent, new SubnetData(s, Location.Default)));
        }

        public ArmBuilder<Subnet, SubnetData> Construct(string name, string cidr, Location location = null, NetworkSecurityGroupData group = null)
        {
            var subnet = new Azure.ResourceManager.Network.Models.Subnet()
            {
                Name = name,
                AddressPrefix = cidr,
            };

            if (null != group)
            {
                subnet.NetworkSecurityGroup = group.Model;
            }

            return new ArmBuilder<Subnet, SubnetData>(this, new SubnetData(subnet, location ?? DefaultLocation));
        }

        public Pageable<SubnetOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.Subnet, SubnetOperations>(
                Operations.List(Id.ResourceGroup, Id.Name, cancellationToken),
                this.convertor());
        }

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
