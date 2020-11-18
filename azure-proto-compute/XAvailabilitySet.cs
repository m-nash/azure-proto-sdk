using azure_proto_core;

namespace azure_proto_compute
{
    public class XAvailabilitySet : AvailabilitySetOperations
    {
        public XAvailabilitySet(ArmClientContext context, PhAvailabilitySet resource):base(context, resource)
        {
            Model = resource;
        }

        public PhAvailabilitySet Model { get; private set; }
    }
}
