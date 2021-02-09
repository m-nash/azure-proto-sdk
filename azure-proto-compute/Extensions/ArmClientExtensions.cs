using Azure.ResourceManager.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public static class ArmClientExtensions
    {
        public static VirtualMachineOperations GetVirtualMachineOperations(this AzureResourceManagerClient client, ResourceIdentifier resourceId)
        {
            return client.GetSubscriptionOperations(resourceId.Subscription).GetResourceGroupOperations(resourceId.ResourceGroup).GetVirtualMachineOperations(resourceId.Name);
        }
    }
}
