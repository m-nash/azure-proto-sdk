using Microsoft.Azure.Management.Subscription.Models;

namespace azure
{
    public class AzureLocation
    {
        public AzureSubscription Subscription { get; private set; }
        private Location location;

        public ResourceGroupCollection ResourceGroups { get; private set; }

        public string Name { get { return location.Name; } }

        public AzureLocation(AzureSubscription subscription, Location location)
        {
            Subscription = subscription;
            this.location = location;
            ResourceGroups = new ResourceGroupCollection(this);
        }
    }
}
