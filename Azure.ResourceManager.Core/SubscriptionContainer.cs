using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Subscription Container Operationss
    /// </summary>
    public class SubscriptionContainer : OperationsBase
    {
        public static readonly ResourceType AzureResourceType = "Microsoft.Resources/subscriptions";

        internal SubscriptionContainer(AzureResourceManagerClientOptions options)
            : base(options, null, null)
        {
        }

        public override ResourceType ResourceType => AzureResourceType;

        internal SubscriptionsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) =>
            new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred, ClientOptions.Convert<ResourcesManagementClientOptions>())).Subscriptions;

        public Pageable<SubscriptionOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<ResourceManager.Resources.Models.Subscription, SubscriptionOperations>(
                Operations.List(cancellationToken),
                Converter());
        }

        public AsyncPageable<SubscriptionOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<ResourceManager.Resources.Models.Subscription, SubscriptionOperations>(
                Operations.ListAsync(cancellationToken),
                Converter());
        }

        private Func<ResourceManager.Resources.Models.Subscription, SubscriptionOperations> Converter()
        {
            return s => new SubscriptionOperations(ClientOptions, new SubscriptionData(s));
        }

        internal async Task<string> GetDefaultSubscription(CancellationToken token = default(CancellationToken))
        {
            var subs = ListAsync(token).GetAsyncEnumerator();
            string sub = null;
            if (await subs.MoveNextAsync())
            {
                if (subs.Current != null)
                {
                    sub = subs.Current.Id.Subscription;
                }
            }

            return sub;
        }
    }
}
