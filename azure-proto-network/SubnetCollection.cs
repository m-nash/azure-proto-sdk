using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System.Collections.Generic;
using System.Threading;

namespace azure_proto_network
{
    /// <summary>
    /// TODO: refactor list methods to include type-specific lists, and allow Child Resources
    /// TODO: refactor list methods for child objects to not have a ListAvailableLocations call
    /// </summary>
    public class SubnetCollection : ResourceCollectionOperations<PhSubnet>
    {
        public SubnetCollection(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetCollection(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";

        public override Pageable<ResourceClientBase<PhSubnet>> List(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return new WrappingPageable<Subnet, ResourceClientBase<PhSubnet>>(Operations.List(Context.ResourceGroup, Context.Parent.Name, cancellationToken), s => new SubnetOperations(this, new PhSubnet(s, DefaultLocation)));
        }

        public override AsyncPageable<ResourceClientBase<PhSubnet>> ListAsync(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return new WrappingAsyncPageable<Subnet, ResourceClientBase<PhSubnet>>(Operations.ListAsync(Context.ResourceGroup, Context.Parent.Name, cancellationToken), s => new SubnetOperations(this, new PhSubnet(s, DefaultLocation)));
        }

        protected override ResourceClientBase<PhSubnet> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new SubnetOperations(this, resource);
        }

        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).Subnets;

    }
}
