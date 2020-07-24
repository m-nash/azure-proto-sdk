using azure_proto_core;
using Microsoft.Azure.Management.ResourceManager;

namespace azure_proto_management
{
    public class AzureResourceGroup : AzureResourceGroupBase
    {
        public AzureResourceGroup(IResource location, PhResourceGroup resourceGroup) : base(location, resourceGroup) { }

        private ResourceManagementClient Client => ClientFactory.Instance.GetResourceClient(Parent.Id);

        public void Delete()
        {
            Client.ResourceGroups.Delete(Name);
        }
    }
}
