using azure_proto_core;

namespace azure_proto_compute
{
    public class XAvailabilitySet : AvailabilitySetOperations
    {
        public XAvailabilitySet(ArmClientContext context, PhAvailabilitySet resource, ArmClientOptions clientOptions): base(context, resource, clientOptions)
        {
            Model = resource;
        }

        public PhAvailabilitySet Model { get; private set; }
    }
}
