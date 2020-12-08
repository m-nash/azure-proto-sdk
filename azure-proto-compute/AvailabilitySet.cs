using azure_proto_core;

namespace azure_proto_compute
{
    public class AvailabilitySet : AvailabilitySetOperations
    {
        public AvailabilitySet(ArmClientContext context, AvailabilitySetData resource):base(context, resource)
        {
            Model = resource;
        }

        public AvailabilitySetData Model { get; private set; }
    }
}
