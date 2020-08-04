using azure_proto_core;

namespace azure_proto_network
{
    public static class AzureResourceGroupExtension
    {
        public static PublicIpAddressCollection IpAddresses(this AzureResourceGroupBase resourceGroup)
        {
            return resourceGroup.GetCollection<PublicIpAddressCollection, AzurePublicIpAddress>(() => { return new PublicIpAddressCollection(resourceGroup); });
        }

        public static VnetCollection VNets(this AzureResourceGroupBase resourceGroup)
        {
            return resourceGroup.GetCollection<VnetCollection, AzureVnet>(() => { return new VnetCollection(resourceGroup); });
        }

        public static NicCollection Nics(this AzureResourceGroupBase resourceGroup)
        {
            return resourceGroup.GetCollection<NicCollection, AzureNic>(() => { return new NicCollection(resourceGroup); });
        }

        public static NetworkSecurityGroupCollection Nsgs(this AzureResourceGroupBase resourceGroup)
        {
            return resourceGroup.GetCollection<NetworkSecurityGroupCollection, AzureNetworkSecurityGroup>(() => { return new NetworkSecurityGroupCollection(resourceGroup); });
        }
    }
}
