using azure_proto_core;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;

namespace azure_proto_management
{
    public class ResourceGroupCollection : AzureCollection<AzureResourceGroup>
    {
        public ResourceGroupCollection(AzureLocation location) : base(location) { }

        public AzureResourceGroup CreateOrUpdate(string resourceGroupName)
        {
            var rmClient = Parent.Clients.ResourceClient;
            var resourceGroup = new ResourceGroup(Parent.Name);
            resourceGroup = rmClient.ResourceGroups.CreateOrUpdateAsync(resourceGroupName, resourceGroup).Result;
            AzureResourceGroup result = new AzureResourceGroup(Parent as AzureLocation, resourceGroup);
            this.Add(resourceGroupName, result);
            return result;
        }

        protected override void LoadValues()
        {
            var rmClient = Parent.Clients.ResourceClient;
            foreach(var rsg in rmClient.ResourceGroups.List())
            {
                this.Add(rsg.Name, new AzureResourceGroup(Parent as AzureLocation, rsg));
            }
        }
    }
}
