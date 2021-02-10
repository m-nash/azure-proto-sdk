using Azure.ResourceManager.Core;
using azure_proto_network;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            return vnetOps.GetSubnetOperations(resourceId.Name);
        }

        /// <summary>
        /// Gets resource operations base.
        /// </summary>
        /// <param name="resourceId"> The Resource Identifier of the resource. </param>
        /// <returns> Resource operations of the resource. </returns>
        public static T GetResourceOperations<T>(this AzureResourceManagerClient client, ResourceIdentifier resourceId)
            where T : OperationsBase
        {
            var rgOp = client.GetSubscriptionOperations(resourceId.Subscription).GetResourceGroupOperations(resourceId.ResourceGroup);
            var resourceType = new ResourceType(resourceId.Parent.Id);
            if (resourceType.Equals(rgOp.Id.Type))
            {
                return Activator.CreateInstance(
                    typeof(T),
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                    null,
                    new object[] { rgOp, resourceId.Name },
                    CultureInfo.InvariantCulture) as T;
            }
            else
            {
                var parentOps = client.GetResourceOperations<VirtualNetworkOperations>(resourceId.Parent.Id);
                return Activator.CreateInstance(
                    typeof(T),
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                    null,
                    new object[] { parentOps, resourceId.Name },
                    CultureInfo.InvariantCulture) as T;
            }
        }
    }
}
