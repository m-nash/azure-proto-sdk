using azure_proto_core;
using Microsoft.Azure.Management.Subscription;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class LocationCollection : AzureCollection<AzureLocation>
    {
        public LocationCollection(AzureSubscription subscription) : base(subscription) { }

        private SubscriptionClient Client => ClientFactory.Instance.GetSubscriptionClient();

        protected override IEnumerable<AzureLocation> GetItems()
        {
            var split = Parent.Id.Split('/');
            foreach (var location in Client.Subscriptions.ListLocations(split[2]))
            {
                yield return new AzureLocation(Parent as AzureSubscription, new PhLocation(location));
            }
        }

        protected override AzureLocation Get(string locationName)
        {
            var split = Parent.Id.Split('/');
            foreach (var location in Client.Subscriptions.ListLocations(split[2]))
            {
                if (location.Name == locationName)
                    return new AzureLocation(Parent as AzureSubscription, new PhLocation(location));
            }
            return null;
        }
    }
}
