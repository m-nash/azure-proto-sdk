using azure_proto_core;
using Azure.ResourceManager.Resources;

namespace azure_proto_management
{
    public partial class AzureResourceGroup : AzureResourceGroupBase
    {
        public AzureResourceGroup(TrackedResource location, PhResourceGroup resourceGroup) : base(resourceGroup.Id, resourceGroup.Location) { }

        private ResourcesManagementClient Client => ClientFactory.Instance.GetResourceClient(Id.Subscription);

        public void Delete()
        {
            Client.ResourceGroups.StartDelete(Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
