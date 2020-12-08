using azure_proto_core;

namespace azure_proto_compute
{
    public class AvailabilitySet : AvailabilitySetOperations
    {
        public AvailabilitySet(ArmClientContext context, AvailabilitySetData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Model = resource;
        }

        public AvailabilitySetData Model { get; private set; }
    }
}
