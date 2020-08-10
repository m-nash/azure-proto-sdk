using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzurePublicIpAddress : AzureOperations<PhPublicIPAddress>
    {
        public AzurePublicIpAddress(TrackedResource resourceGroup, PhPublicIPAddress ip) :base( resourceGroup, ip)
        {
        }
    }
}
