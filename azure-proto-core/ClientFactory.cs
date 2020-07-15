using Azure.Identity;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Network;
using Microsoft.Azure.Management.Subscription;

namespace azure_proto_core
{
    public class ClientFactory
    {
        private string subscriptionId;

        public ClientFactory(string subscriptionId)
        {
            this.subscriptionId = subscriptionId;
        }

        private ComputeManagementClient computeClient;
        public ComputeManagementClient ComputeClient
        {
            get
            {
                if (computeClient == null)
                {
                    computeClient = new ComputeManagementClient(subscriptionId, new DefaultAzureCredential());
                }
                return computeClient;
            }
        }

        private NetworkManagementClient networkClient;
        public NetworkManagementClient NetworkClient
        {
            get
            {
                if (networkClient == null)
                {
                    networkClient = new NetworkManagementClient(subscriptionId, new DefaultAzureCredential());
                }
                return networkClient;
            }
        }

        private Microsoft.Azure.Management.ResourceManager.ResourceManagementClient resourceClient;
        public Microsoft.Azure.Management.ResourceManager.ResourceManagementClient ResourceClient
        {
            get
            {
                if (resourceClient == null)
                {
                    resourceClient = new Microsoft.Azure.Management.ResourceManager.ResourceManagementClient(AzureClientManager.Instance.Creds);
                    resourceClient.SubscriptionId = subscriptionId;
                }
                return resourceClient;
            }
        }

        public static SubscriptionClient SubscriptionClient { get { return AzureClientManager.Instance.SubscriptionClient; } }
    }
}
