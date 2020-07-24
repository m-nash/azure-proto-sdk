using azure_proto_core;

namespace azure_proto_network
{
    public class AzurePublicIpAddress : AzureResource
    {
        public AzurePublicIpAddress(IResource resourceGroup, PhPublicIPAddress ip) : base(resourceGroup, ip) { }
    }
}
