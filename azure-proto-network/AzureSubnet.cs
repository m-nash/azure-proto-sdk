using azure_proto_core;

namespace azure_proto_network
{
    public class AzureSubnet : AzureResource
    {
        public AzureSubnet(AzureVnet vnet, PhSubnet model) : base(vnet, model) { }
    }
}
