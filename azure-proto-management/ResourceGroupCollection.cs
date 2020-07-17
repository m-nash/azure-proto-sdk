using azure_proto_core;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using System.Collections.Generic;

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
            AzureResourceGroup result = new AzureResourceGroup(Parent, new PhResourceGroup(resourceGroup));
            return result;
        }

        public override IEnumerable<AzureResourceGroup> GetItems()
        {
            var rmClient = Parent.Clients.ResourceClient;
            foreach (var rsg in rmClient.ResourceGroups.List())
            {
                yield return new AzureResourceGroup(Parent, new PhResourceGroup(rsg));
            }
        }

        protected override AzureResourceGroup Get(string resourceGroupName)
        {
            var rmClient = Parent.Clients.ResourceClient;
            var rgResult = rmClient.ResourceGroups.Get(resourceGroupName);
            return new AzureResourceGroup(Parent, new PhResourceGroup(rgResult));
        }
    }
}
