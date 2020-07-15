using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute
{
    public class AzureAvailabilitySet : AzureResource<AvailabilitySet>
    {
        public override string Name => Model.Name;

        public override string Id => Model.Id;

        public AzureAvailabilitySet(IResource parent, AvailabilitySet model):base(parent, model) { }
    }
}
