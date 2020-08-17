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
    public class PublicIpContainer : ArmResourceContainerOperations<PhPublicIPAddress, ArmOperation<PhPublicIPAddress>>
    {
        public PublicIpContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public PublicIpContainer(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public override ArmOperation<PhPublicIPAddress> Create(string name, PhPublicIPAddress resourceDetails)
        {
            return new PhArmOperation<PhPublicIPAddress, PublicIPAddress>(Operations.StartCreateOrUpdate(Context.ResourceGroup, name, resourceDetails), n => new PhPublicIPAddress(n));
        }

        public async override Task<ArmOperation<PhPublicIPAddress>> CreateAsync(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PhPublicIPAddress, PublicIPAddress>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, name, resourceDetails, cancellationToken), n => new PhPublicIPAddress(n));
        }

        public PhPublicIPAddress ConstructIPAddress(Location location = null)
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = Azure.ResourceManager.Network.Models.IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = location ?? DefaultLocation,
            };

            return new PhPublicIPAddress(ipAddress);
        }


        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).PublicIPAddresses;

    }
}
