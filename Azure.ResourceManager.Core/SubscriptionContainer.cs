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

        internal SubscriptionContainer(ArmClientContext context, ArmClientOptions options)
            : base(context, null, options, null)
        {
        }

        public override ResourceType ResourceType => AzureResourceType;

        internal SubscriptionsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) =>
            new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred, ArmClientOptions.Convert<ResourcesManagementClientOptions>(ClientOptions))).Subscriptions;

        public Pageable<SubscriptionOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.Subscription, SubscriptionOperations>(
                Operations.List(cancellationToken),
                Converter());
        }

        public AsyncPageable<SubscriptionOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.Subscription, SubscriptionOperations>(
                Operations.ListAsync(cancellationToken),
                Converter());
        }

        private Func<Azure.ResourceManager.Resources.Models.Subscription, SubscriptionOperations> Converter()
        {
            return s => new SubscriptionOperations(ClientContext, new SubscriptionData(s), ClientOptions);
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
