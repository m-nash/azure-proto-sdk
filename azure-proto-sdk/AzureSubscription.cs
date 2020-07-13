using Azure.Identity;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Network;
using Microsoft.Azure.Management.Subscription.Models;

namespace azure
{
    public class AzureSubscription
    {
        private SubscriptionModel subModel;

        public AzureClient Client { get; private set; }

        public LocationCollection Locations { get; private set; }

        private ComputeManagementClient computeClient;
        internal ComputeManagementClient ComputeClient
        {
            get
            {
                if (computeClient == null)
                {
                    computeClient = new ComputeManagementClient(Id, new DefaultAzureCredential());
                }
                return computeClient;
            }
        }

        private NetworkManagementClient networkClient;
        internal NetworkManagementClient NetworkClient
        {
            get
            {
                if (networkClient == null)
                {
                    networkClient = new NetworkManagementClient(Id, new DefaultAzureCredential());
                }
                return networkClient;
            }
        }

        private Microsoft.Azure.Management.ResourceManager.ResourceManagementClient resourceClient;
        internal Microsoft.Azure.Management.ResourceManager.ResourceManagementClient ResourceClient
        {
            get
            {
                if (resourceClient == null)
                {
                    resourceClient = new Microsoft.Azure.Management.ResourceManager.ResourceManagementClient(AzureClientManager.Instance.Creds);
                    resourceClient.SubscriptionId = Id;
                }
                return resourceClient;
            }
        }

        public AzureSubscription(AzureClient client, SubscriptionModel subModel)
        {
            this.subModel = subModel;
            Client = client;
            Locations = new LocationCollection(this);
        }

        public string Id { get { return subModel.SubscriptionId; } }
    }
}
