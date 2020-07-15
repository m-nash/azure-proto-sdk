using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureSubnet : AzureResource
    {
        public AzureSubnet(AzureVnet vnet, PhSubnet model) : base(vnet, model) { }

        public override string Name => Model.Name;

        public override string Id => Model.Id;
    }
}
