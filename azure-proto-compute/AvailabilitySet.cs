using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    public class AvailabilitySet : AvailabilitySetOperations
    {
        public AvailabilitySet(ResourceOperationsBase operations, AvailabilitySetData resource)
            : base(operations, resource.Id)
        {
            Data = resource;
        }

        public AvailabilitySetData Data { get; private set; }
    }
}
