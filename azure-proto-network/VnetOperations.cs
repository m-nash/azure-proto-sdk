using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
    /// Virtual Network Operations
    /// TODO: Verify that DefaultLocation is correctly plumbed through
    /// </summary>
    public class VnetOperations : ResourceOperations<PhVirtualNetwork>
    {
        public VnetOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VnetOperations(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Context.ResourceGroup, Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Context.ResourceGroup, Context.Name, cancellationToken));
        }

        public override Response<ResourceOperations<PhVirtualNetwork>> Get()
        {
            return new PhArmResponse<ResourceOperations<PhVirtualNetwork>, VirtualNetwork>(Operations.Get(Context.ResourceGroup, Context.Name), 
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public async override Task<Response<ResourceOperations<PhVirtualNetwork>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperations<PhVirtualNetwork>, VirtualNetwork>(await Operations.GetAsync(Context.ResourceGroup, Context.Name, null, cancellationToken),
                n => { Resource = new PhVirtualNetwork(n); return this;});
        }

        public override ArmOperation<ResourceOperations<PhVirtualNetwork>> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<ResourceOperations<PhVirtualNetwork>, VirtualNetwork>(Operations.UpdateTags(Context.ResourceGroup, Context.Name, patchable),
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public async override Task<ArmOperation<ResourceOperations<PhVirtualNetwork>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<ResourceOperations<PhVirtualNetwork>, VirtualNetwork>(await Operations.UpdateTagsAsync(Context.ResourceGroup, Context.Name, patchable, cancellationToken),
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public SubnetOperations Subnet(TrackedResource subnet)
        {
            return new SubnetOperations(this, subnet);
        }

        public SubnetOperations Subnet(ResourceIdentifier subnet)
        {
            return new SubnetOperations(this, subnet);
        }

        public SubnetOperations Subnet(string subnet)
        {
            return new SubnetOperations(this, $"{this.Context}/subnets/{subnet}");
        }

        public PhSubnet ConstructSubnet(string name, string cidr, Location location = null, PhNetworkSecurityGroup group = null)
        {
            var subnet = new Subnet()
            {
                Name = name,
                AddressPrefix = cidr,
            };

            if (null != group)
            {
                subnet.NetworkSecurityGroup = group.Model;
            }

            return new PhSubnet(subnet, location ?? DefaultLocation);
        }

        public ArmOperation<ResourceOperations<PhSubnet>> CreateSubnet(string name, PhSubnet resourceDetails)
        {
            return GetSubnetContainer().Create(name, resourceDetails);
        }

        public Task<ArmOperation<ResourceOperations<PhSubnet>>> CreateSubnetAsync(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            return GetSubnetContainer().CreateAsync(name, resourceDetails, cancellationToken);
        }

        public Pageable<ResourceOperations<PhSubnet>> ListSubnets(CancellationToken cancellationToken = default)
        {
            return GetSubnetCollection().List(null, null, cancellationToken);
        }

        public AsyncPageable<ResourceOperations<PhSubnet>> ListSubnetsAsync(CancellationToken cancellationToken = default)
        {
            return GetSubnetCollection().ListAsync(null, null, cancellationToken);
        }

        internal SubnetContainer GetSubnetContainer()
        {
            return new SubnetContainer(this, Context);
        }

        internal SubnetCollection GetSubnetCollection()
        {
            return new SubnetCollection(this, Context);
        }


        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).VirtualNetworks;
    }
}
