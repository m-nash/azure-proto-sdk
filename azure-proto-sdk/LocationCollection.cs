using Microsoft.Azure.Management.Subscription;
using System.Collections.Generic;

namespace azure_proto_sdk
{
    public class LocationCollection : Dictionary<string, AzureLocation>
    {
        private AzureSubscription subscription;
        private bool initialized;

        public LocationCollection(AzureSubscription subscription)
        {
            this.subscription = subscription;
        }

        new public AzureLocation this[string key]
        {
            get
            {
                //lazy load on first access
                if (!this.ContainsKey(key) && !initialized)
                {
                    var subscriptionClient = AzureClientManager.Instance.SubscriptionClient;
                    foreach (var location in subscriptionClient.Subscriptions.ListLocations(subscription.Id))
                    {
                        this.Add(location.Name, new AzureLocation(subscription, location));
                    }
                    initialized = true;
                }
                AzureLocation value;
                return this.TryGetValue(key, out value) ? value : null;
            }
            set { /*disable setting values*/ }
        }
    }
}
