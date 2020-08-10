using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureSubnet : AzureOperations<PhSubnet>
    {
        public AzureSubnet(AzureVnet vnet, PhSubnet model) : base(vnet, model)
        {
        }
    }
}
