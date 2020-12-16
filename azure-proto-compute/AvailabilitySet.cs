using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    public class AvailabilitySet : AvailabilitySetOperations
    {
        public AvailabilitySet(AzureResourceManagerClientContext context, AvailabilitySetData resource, AzureResourceManagerClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public AvailabilitySetData Data { get; private set; }
    }
}
