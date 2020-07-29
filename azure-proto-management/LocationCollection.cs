using Azure.ResourceManager.Resources;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class LocationCollection : AzureCollection<AzureLocation>
    {
        public LocationCollection(AzureSubscription subscription) : base(subscription) { }

        private SubscriptionsOperations Client => ClientFactory.Instance.GetSubscriptionClient();

        protected override IEnumerable<AzureLocation> GetItems()
        {
            foreach (var location in Client.ListLocations(Parent.Id.Subscription))
            {
                yield return new AzureLocation(Parent as AzureSubscription, new PhLocation(location));
            }
        }

        protected override AzureLocation Get(string locationName)
        {
            foreach (var location in Client.ListLocations(Parent.Id.Subscription))
            {
                if (location.Name == locationName)
                    return new AzureLocation(Parent as AzureSubscription, new PhLocation(location));
            }
            return null;
        }
    }
}
