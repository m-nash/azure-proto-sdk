using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute
{
    public class AzureAvailabilitySet : AzureResource<AvailabilitySet>
    {
        public AzureAvailabilitySet(TrackedResource parent, PhAvailabilitySet model):base(model.Id, model.Location) 
        {
            Data = model.Data;
        }

        public override AvailabilitySet Data { get; protected set; }
    }
}
