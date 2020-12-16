using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    public class AvailabilitySet : AvailabilitySetOperations
    {
        public AvailabilitySet(ArmClientContext context, AvailabilitySetData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public AvailabilitySetData Data { get; private set; }
    }
}
