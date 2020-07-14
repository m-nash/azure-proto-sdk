using Microsoft.Azure.Management.Subscription.Models;

namespace azure_proto_sdk.Management
{
    public class AzureLocation: AzureResource<AzureSubscription, Location>
    {
        public ResourceGroupCollection ResourceGroups { get; private set; }

        public string Name { get { return Model.Name; } }

        public AzureLocation(AzureSubscription subscription, Location location) : base(subscription, location)
        {
            ResourceGroups = new ResourceGroupCollection(this);
        }
    }
}
