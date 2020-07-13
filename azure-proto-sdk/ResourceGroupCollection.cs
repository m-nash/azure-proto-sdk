using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;

namespace azure
{
    public class ResourceGroupCollection : AzureCollection<AzureResourceGroup>
    {
        public AzureLocation Location { get; private set; }

        public ResourceGroupCollection(AzureLocation location)
        {
            Location = location;
        }

        internal AzureResourceGroup CreateOrUpdate(string resourceGroupName)
        {
            var rmClient = Location.Subscription.ResourceClient;
            var resourceGroup = new ResourceGroup(Location.Name);
            resourceGroup = rmClient.ResourceGroups.CreateOrUpdateAsync(resourceGroupName, resourceGroup).Result;
            AzureResourceGroup result = new AzureResourceGroup(Location, resourceGroup);
            this.Add(resourceGroupName, result);
            return result;
        }

        protected override void LoadValues()
        {
            var rmClient = Location.Subscription.ResourceClient;
            foreach(var rsg in rmClient.ResourceGroups.List())
            {
                this.Add(rsg.Name, new AzureResourceGroup(Location, rsg));
            }
        }
    }
}
