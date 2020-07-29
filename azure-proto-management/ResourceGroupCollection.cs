using azure_proto_core;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class ResourceGroupCollection : AzureCollection<AzureResourceGroup>
    {
        public ResourceGroupCollection(AzureSubscription location) : base(location) { }

        private ResourcesManagementClient Client => ClientFactory.Instance.GetResourceClient(Parent.Id.Subscription);

        public AzureResourceGroup CreateOrUpdate(string resourceGroupName, string location)
        {
            var resourceGroup = new ResourceGroup(location);
            resourceGroup = Client.ResourceGroups.CreateOrUpdateAsync(resourceGroupName, resourceGroup).Result;
            AzureResourceGroup result = new AzureResourceGroup(Parent, new PhResourceGroup(resourceGroup));
            return result;
        }

        protected override IEnumerable<AzureResourceGroup> GetItems()
        {
            foreach (var rsg in Client.ResourceGroups.List())
            {
                yield return new AzureResourceGroup(Parent, new PhResourceGroup(rsg));
            }
        }

        protected override AzureResourceGroup Get(string resourceGroupName)
        {
            var rgResult = Client.ResourceGroups.Get(resourceGroupName);
            return new AzureResourceGroup(Parent, new PhResourceGroup(rgResult));
        }
    }
}
