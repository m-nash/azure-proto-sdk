using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzurePublicIpAddress : AzureEntity<PublicIPAddress>
    {
        public AzurePublicIpAddress(TrackedResource resourceGroup, PhPublicIPAddress ip) :base( ip.Id, ip.Location)
        {
            Data = ip.Data;
        }

        public override PublicIPAddress Data { get ; protected set ; }
    }
}
