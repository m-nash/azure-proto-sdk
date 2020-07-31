using azure_proto_core;
using Azure.ResourceManager.Resources;
using System.Threading.Tasks;
using System.Threading;

namespace azure_proto_management
{
    public partial class AzureResourceGroup : AzureResourceGroupBase
    {
        public AzureResourceGroup(TrackedResource parent, PhResourceGroup resourceGroup) : base(parent, resourceGroup) { }

        private ResourcesManagementClient Client => ClientFactory.Instance.GetResourceClient(Id.Subscription);

        public void Delete()
        {
            Client.ResourceGroups.StartDelete(Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task DeleteAsync(CancellationToken cancellationToken = default)
        {
            await Client.ResourceGroups.StartDeleteAsync(Name, cancellationToken);
        }
    }
}
