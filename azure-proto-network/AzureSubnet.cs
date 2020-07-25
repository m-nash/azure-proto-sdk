using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureSubnet : AzureEntity<Subnet>
    {
        public AzureSubnet(AzureVnet vnet, PhSubnet model) : base(model.Id, vnet.Location)
        {
            Data = model.Data;
        }

        public override Subnet Data { get; protected set; }
    }
}
