using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureNic : AzureResource<NetworkInterface>
    {
        public AzureNic(IResource resourceGroup, NetworkInterface nic) : base(resourceGroup, nic) { }

        public override string Name => Model.Name;

        public override string Id => Model.Id;
    }
}
