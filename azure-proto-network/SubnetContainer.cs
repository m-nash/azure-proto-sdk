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
    public class SubnetContainer : ResourceContainerOperations<PhSubnet, ArmOperation<PhSubnet>>
    {
        public SubnetContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetContainer(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";


        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).Subnets;

        public override ArmOperation<PhSubnet> Create(string name, PhSubnet resourceDetails)
        {
            return new PhArmOperation<PhSubnet, Subnet>(Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, name, resourceDetails.Model), s => new PhSubnet(s, Location.Default));
        }

        public async override Task<ArmOperation<PhSubnet>> CreateAsync(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PhSubnet, Subnet>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Name, name, resourceDetails.Model, cancellationToken), s => new PhSubnet(s, Location.Default));
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
    }
}
