using Azure.ResourceManager.Compute;
using azure_proto_core;

namespace azure_proto_compute
{
    public static class ClientFactoryExtension
    {
        public static ComputeManagementClient GetComputeClient(this ClientFactory factory, string subscriptionId)
        {
            return factory.GetClient(subscriptionId, (subscriptionId, credentials) => { return new ComputeManagementClient(subscriptionId, credentials); });
        }
    }
}
