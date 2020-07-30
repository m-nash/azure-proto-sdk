using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public class AvailabilitySetCollection : AzureCollection<AzureAvailabilitySet>
    {
        public AvailabilitySetCollection(TrackedResource resourceGroup) : base(resourceGroup) { }

        private ComputeManagementClient Client => ClientFactory.Instance.GetComputeClient(Parent.Id.Subscription);

        public AzureAvailabilitySet CreateOrUpdateAvailabilityset(string name, AzureAvailabilitySet availabilitySet)
        {
            var aSet = Client.AvailabilitySets.CreateOrUpdate(Parent.Name, name, availabilitySet.Model);
            AzureAvailabilitySet azureAvailabilitySet = new AzureAvailabilitySet(Parent, new PhAvailabilitySet(aSet.Value));
            return azureAvailabilitySet;
        }

        protected override AzureAvailabilitySet Get(string availabilitySetName)
        {
            var asetResult = Client.AvailabilitySets.Get(Parent.Name, availabilitySetName);
            return new AzureAvailabilitySet(Parent, new PhAvailabilitySet(asetResult.Value));
        }

        protected override IEnumerable<AzureAvailabilitySet> GetItems()
        {
            foreach (var aset in Client.AvailabilitySets.List(Parent.Name))
            {
                yield return new AzureAvailabilitySet(Parent, new PhAvailabilitySet(aset));
            }
        }

        public AzureAvailabilitySet ConstructAvailabilitySet(string skuName)
        {
            var availabilitySet = new AvailabilitySet(Parent.Location)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName },
            };
            return new AzureAvailabilitySet(Parent, new PhAvailabilitySet(availabilitySet));
        }
    }
}
