using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureNic : AzureResource
    {
        public AzureNic(IResource resourceGroup, PhNetworkInterface nic) : base(resourceGroup, nic) { }

        public override string Name => Model.Name;

        public override string Id => Model.Id;
    }
}
