using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute
{
    public class AzureAvailabilitySet : AzureResource
    {
        public override string Name => Model.Name;

        public override string Id => Model.Id;

        public AzureAvailabilitySet(IResource parent, PhAvailabilitySet model):base(parent, model) { }
    }
}
