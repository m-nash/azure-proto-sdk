using Azure.ResourceManager.Compute.Models;

namespace azure_proto_sdk
{
    public class AzureAvailabilitySet
    {
        private AvailabilitySet availabilitySet;

        public AzureResourceGroup ResourceGroup { get; private set; }

        public string Id { get { return availabilitySet.Id; } }

        public AzureAvailabilitySet(AzureResourceGroup resourceGroup, AvailabilitySet availabilitySet)
        {
            ResourceGroup = resourceGroup;
            this.availabilitySet = availabilitySet;
        }
    }
}
