using azure_proto_core;
using Microsoft.Azure.Management.Subscription.Models;

namespace azure_proto_management
{
    public class AzureLocation: AzureResource<Location>
    {
        public ResourceGroupCollection ResourceGroups { get; private set; }

        public override string Name => Model.Name;

        public override string Id => Model.Id;

        public AzureLocation(AzureSubscription subscription, Location location) : base(subscription, location)
        {
            ResourceGroups = new ResourceGroupCollection(this);
        }
    }
}
