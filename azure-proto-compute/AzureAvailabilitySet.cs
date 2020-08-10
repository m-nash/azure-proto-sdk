using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute
{
    public class AzureAvailabilitySet : AzureOperations<PhAvailabilitySet>
    {
        public AzureAvailabilitySet(TrackedResource parent, PhAvailabilitySet model):base(parent, model) 
        {
        }

    }
}
