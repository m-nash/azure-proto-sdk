using Azure.ResourceManager.Compute.Models;
using azure_proto_sdk.Management;

namespace azure_proto_sdk.Compute
{
    public class AzureAvailabilitySet : AzureResource<AzureResourceGroup, AvailabilitySet>
    {
        public string Id { get { return Model.Id; } }

        public AzureAvailabilitySet(AzureResourceGroup parent, AvailabilitySet model):base(parent, model) { }
    }
}
