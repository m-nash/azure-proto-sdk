using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureNic : AzureResource<NetworkInterface>
    {
        public AzureNic(TrackedResource resourceGroup, PhNetworkInterface nic) : base(nic.Id, nic.Location) 
        { Data = nic.Data; }

        public override NetworkInterface Data { get; protected set; }
    }
}
