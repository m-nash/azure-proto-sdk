using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    public class AvailabilitySet : AvailabilitySetOperations
    {
        public AvailabilitySet(AzureResourceManagerClientContext context, AvailabilitySetData resource)
            : base(context, resource.Id)
        {
            Data = resource;
        }

        public AvailabilitySetData Data { get; private set; }
    }
}
