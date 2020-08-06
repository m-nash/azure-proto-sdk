using azure_proto_core;

namespace azure_proto_network
{
    public class AzureNic : AzureEntity<PhNetworkInterface>
    {
        public AzureNic(TrackedResource resourceGroup, PhNetworkInterface nic) : base(resourceGroup, nic) { }
    }
}
