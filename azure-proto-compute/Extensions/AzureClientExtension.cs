using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public static class AzureClientExtension
    {
        public static IEnumerable<AzureVm> Vms(this AzureClientBase client)
        {
            return client.GetSubscriptions<VmCollection, AzureVm>((rg) => { return new VmCollection(rg); });
        }
    }
}
