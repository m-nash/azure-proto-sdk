using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public static class AzureSubscriptionExtension
    {
        public static IEnumerable<AzureVm> Vms(this AzureSubscriptionBase subscription)
        {
            return subscription.GetResources<VmCollection, AzureVm>((rg)=> { return new VmCollection(rg); });
        }
    }
}
