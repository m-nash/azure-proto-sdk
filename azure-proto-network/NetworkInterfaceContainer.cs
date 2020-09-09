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
        public NetworkInterfaceContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkInterfaceContainer(ArmClientContext parent, TrackedResource context) : base(parent, context)
        {
        }

        public NetworkInterfaceContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkInterfaceContainer(OperationsBase parent, TrackedResource context) : base(parent, context)
        {
            var operation = Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, resourceDetails);
            return new PhArmOperation<ResourceClientBase<PhNetworkInterface>, Azure.ResourceManager.Network.Models.NetworkInterface>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public override ArmOperation<ResourceOperationsBase<PhNetworkInterface>> Create(string name, PhNetworkInterface resourceDetails)
        {
            return new PhArmOperation<ResourceOperationsBase<PhNetworkInterface>, Azure.ResourceManager.Network.Models.NetworkInterface>(
                Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, resourceDetails),
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhNetworkInterface>>> CreateAsync(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhNetworkInterface>, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Name, resourceDetails, cancellationToken), 
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).NetworkInterfaces;
    }
}
