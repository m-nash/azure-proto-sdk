using azure_proto_core;

namespace azure_proto_compute
{
    public class AzureAvailabilitySet : AzureResource
    {
        public AzureAvailabilitySet(IResource parent, PhAvailabilitySet model):base(parent, model) { }
    }
}
