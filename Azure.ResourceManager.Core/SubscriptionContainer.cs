using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Resources;
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
        internal SubscriptionContainer(AzureResourceManagerClientOptions options)
            : base(options, null, null)
        {
        }

        internal SubscriptionsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) =>
            new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred, ClientOptions.Convert<ResourcesManagementClientOptions>())).Subscriptions;

        protected override ResourceType ValidResourceType => SubscriptionOperations.ResourceType;

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

        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier != null)
                throw new ArgumentException("Invalid parent for subscription container");
        }
    }
}
