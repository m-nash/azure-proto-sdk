using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    public class AvailabilitySet : AvailabilitySetOperations
    {
        public AvailabilitySet(AzureResourceManagerClientOptions options, AvailabilitySetData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        public AvailabilitySetData Data { get; private set; }
    }
}
