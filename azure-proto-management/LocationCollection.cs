﻿using azure_proto_core;
using Microsoft.Azure.Management.Subscription;

namespace azure_proto_management
{ 
    public class LocationCollection : AzureCollection<AzureLocation>
    {
        public LocationCollection(AzureSubscription subscription) : base(subscription) { }

        protected override AzureLocation GetSingleValue(string key)
        {
            SubscriptionClient subscriptionClient = ClientFactory.SubscriptionClient;
            var split = Parent.Id.Split('/');
            foreach (var location in subscriptionClient.Subscriptions.ListLocations(split[2]))
            {
                if (location.Name == key)
                    return new AzureLocation(Parent as AzureSubscription, new PhLocation(location));
            }
            return null;
        }

        protected override void LoadValues()
        {
            SubscriptionClient subscriptionClient = ClientFactory.SubscriptionClient;
            var split = Parent.Id.Split('/');
            foreach (var location in subscriptionClient.Subscriptions.ListLocations(split[2]))
            {
                this.Add(location.Name, new AzureLocation(Parent as AzureSubscription, new PhLocation(location)));
            }
        }
    }
}
