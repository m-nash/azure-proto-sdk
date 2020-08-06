using azure_proto_core;

namespace azure_proto_compute
{
    public class AzureAvailabilitySet : AzureEntity<PhAvailabilitySet>
    {
        public AzureAvailabilitySet(TrackedResource parent, PhAvailabilitySet model):base(parent, model) 
        {
        }

    }
}
