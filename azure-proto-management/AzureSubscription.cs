using azure_proto_core;
using Microsoft.Azure.Management.Subscription.Models;

namespace azure_proto_management
{
    public class AzureSubscription : AzureResource
    {
        public LocationCollection Locations { get; private set; }

        public AzureSubscription(AzureClient client, PhSubscriptionModel subModel) : base(client, subModel)
        {
            Locations = new LocationCollection(this);
        }

        public override string Name => (Model as SubscriptionModel).DisplayName;

        public override string Id => Model.Id;
    }
}
