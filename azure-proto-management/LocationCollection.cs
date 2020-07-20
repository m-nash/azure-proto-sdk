using azure_proto_core;
using Microsoft.Azure.Management.Subscription;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class LocationCollection : AzureCollection<AzureLocation>
    {
        public LocationCollection(AzureSubscription subscription) : base(subscription) { }

        protected override IEnumerable<AzureLocation> GetItems()
        {
            SubscriptionClient subscriptionClient = ClientFactory.SubscriptionClient;
            var split = Parent.Id.Split('/');
            foreach (var location in subscriptionClient.Subscriptions.ListLocations(split[2]))
            {
                yield return new AzureLocation(Parent as AzureSubscription, new PhLocation(location));
            }
        }

        protected override AzureLocation Get(string locationName)
        {
            SubscriptionClient subscriptionClient = ClientFactory.SubscriptionClient;
            var split = Parent.Id.Split('/');
            foreach (var location in subscriptionClient.Subscriptions.ListLocations(split[2]))
            {
                if (location.Name == locationName)
                    return new AzureLocation(Parent as AzureSubscription, new PhLocation(location));
            }
            return null;
        }
    }
}
