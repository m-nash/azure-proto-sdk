using Azure.ResourceManager.Core;
using azure_proto_network;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public static class ArmClientExtensions
    {
        public static SubnetOperations GetSubnetOperations(this AzureResourceManagerClient client, ResourceIdentifier resourceId)
        {
            var subOps = client.GetSubscriptionOperations(resourceId.Subscription);
            var rgOps = subOps.GetResourceGroupOperations(resourceId.ResourceGroup);
            var vnetOps = rgOps.GetVirtualNetworkOperations(resourceId.Parent.Name);
            return vnetOps.Subnet(resourceId.Name);
        }
    }
}
