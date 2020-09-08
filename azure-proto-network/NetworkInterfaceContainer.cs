using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkInterfaceContainer : ResourceContainerOperations<PhNetworkInterface>
    {
        public NetworkInterfaceContainer(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkInterfaceContainer(ArmClientBase parent, TrackedResource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public override ArmOperation<ResourceClientBase<PhNetworkInterface>> Create(string name, PhNetworkInterface resourceDetails)
        {
            var operation = Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, resourceDetails);
            return new PhArmOperation<ResourceClientBase<PhNetworkInterface>, Azure.ResourceManager.Network.Models.NetworkInterface>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        public async override Task<ArmOperation<ResourceClientBase<PhNetworkInterface>>> CreateAsync(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceClientBase<PhNetworkInterface>, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Name, resourceDetails, cancellationToken), 
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).NetworkInterfaces;
    }
}
