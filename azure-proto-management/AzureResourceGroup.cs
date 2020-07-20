using azure_proto_core;
using Microsoft.Azure.Management.ResourceManager;

namespace azure_proto_management
{
    public class AzureResourceGroup : AzureResourceGroupBase
    {
        public AzureResourceGroup(IResource location, PhResourceGroup resourceGroup) : base(location, resourceGroup) { }

        public void Delete()
        {
            var resourceClient = Clients.ResourceClient;
            resourceClient.ResourceGroups.Delete(Name);
        }
    }
}
