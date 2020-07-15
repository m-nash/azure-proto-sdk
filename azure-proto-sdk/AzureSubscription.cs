using azure_proto_core;
using Microsoft.Azure.Management.Subscription.Models;

namespace azure_proto_management
{
    public class AzureSubscription : AzureResource<SubscriptionModel>
    {
        public LocationCollection Locations { get; private set; }

        public AzureSubscription(AzureClient client, SubscriptionModel subModel) : base(client, subModel)
        {
            Locations = new LocationCollection(this);
        }

        public override string Name => Model.DisplayName;

        public override string Id => Model.Id;
    }
}
