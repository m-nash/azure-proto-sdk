using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace azure_proto_management
{
    public class ArmClient : AzureOperations
    {
        public string DefaultSubscription { get; set; }

        internal async Task<string> GetDefaultSubscription(CancellationToken token = default(CancellationToken))
        {
            string sub = DefaultSubscription;
            if (null == sub)
            {
                var subs = ListSubscriptionsAsync(token).GetAsyncEnumerator();
                if (await subs.MoveNextAsync())
                {
                    sub = subs.Current?.SubscriptionId;
                }
            }

            return sub;
        }

        public AsyncPageable<PhSubscriptionModel> ListSubscriptionsAsync(CancellationToken token = default(CancellationToken))
        {
            return new WrappingAsyncPageable<Subscription, PhSubscriptionModel>(SubscriptionsClient.ListAsync(token), s => new PhSubscriptionModel(s));
        }

        public Pageable<PhSubscriptionModel> ListSubscriptions(CancellationToken token = default(CancellationToken))
        {
            return new WrappingPageable<Subscription, PhSubscriptionModel>(SubscriptionsClient.List(token), s => new PhSubscriptionModel(s));
        }

        public AsyncPageable<PhLocation> ListLocationsAsync(string subscriptionId = null, CancellationToken token = default(CancellationToken))
        {
            async Task<AsyncPageable<PhLocation>> PageableFunc()
            {
                if (string.IsNullOrWhiteSpace(subscriptionId))
                {
                    subscriptionId = (await GetDefaultSubscription(token));
                    if (subscriptionId == null)
                    {
                        throw new InvalidOperationException("Please select a default subscription");
                    }
                }

                return new WrappingAsyncPageable<Azure.ResourceManager.Resources.Models.Location, PhLocation>(SubscriptionsClient.ListLocationsAsync(subscriptionId, token), s => new PhLocation(s));

            }

            return new TaskDeferringAsyncPageable<PhLocation>(PageableFunc);
        }


        public Pageable<PhLocation> ListLocations(string subscriptionId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                subscriptionId = GetDefaultSubscription(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult();
                if (subscriptionId == null)
                {
                    throw new InvalidOperationException("Please select a default subscription");
                }
            }

            return new WrappingPageable<Azure.ResourceManager.Resources.Models.Location, PhLocation>(SubscriptionsClient.ListLocations(subscriptionId, token), s => new PhLocation(s));
        }


        public async IAsyncEnumerable<azure_proto_core.Location> ListAvailableLocationsAsync(ResourceType resourceType, [EnumeratorCancellation] CancellationToken cancellationToken = default(CancellationToken))
        {
            var subscriptionId = (await GetDefaultSubscription(cancellationToken));
            if (subscriptionId == null)
            {
                throw new InvalidOperationException("Please select a default subscription");
            }

            await foreach (var location in ListAvailableLocationsAsync(subscriptionId, resourceType, cancellationToken))
            {
                yield return location;
            }
        }

        public async IAsyncEnumerable<azure_proto_core.Location> ListAvailableLocationsAsync(string subscription, ResourceType resourceType, [EnumeratorCancellation] CancellationToken cancellationToken = default(CancellationToken))
        {
            await foreach (var provider in GetResourcesClient(subscription).Providers.ListAsync(expand: "metadata", cancellationToken: cancellationToken).WithCancellation(cancellationToken))
            {
                if (string.Equals(provider.Namespace, resourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                {
                    var foundResource = provider.ResourceTypes.FirstOrDefault(p => resourceType.Equals(p.ResourceType));
                    foreach (var location in foundResource.Locations)
                    {
                        yield return new azure_proto_core.Location(location);
                    }
                }
            }
        }

        public IEnumerable<azure_proto_core.Location> ListAvailableLocations(ResourceType resourceType, CancellationToken cancellationToken = default(CancellationToken))
        {
            var subscription = GetDefaultSubscription().ConfigureAwait(false).GetAwaiter().GetResult();
            return ListAvailableLocations(subscription, resourceType, cancellationToken);
        }

        public IEnumerable<azure_proto_core.Location> ListAvailableLocations(string subscription, ResourceType resourceType, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetResourcesClient(subscription).Providers.List(expand: "metadata", cancellationToken: cancellationToken)
                .FirstOrDefault(p => string.Equals(p.Namespace, resourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                .ResourceTypes.FirstOrDefault(r => resourceType.Equals(r.ResourceType))
                .Locations.Select(l => new azure_proto_core.Location(l));
        }




        internal SubscriptionsOperations SubscriptionsClient => ClientFactory.Instance.GetSubscriptionClient();

        internal ResourcesManagementClient GetResourcesClient(string subscription) => ClientFactory.Instance.GetResourceClient(subscription);

        public ArmClient()
        {
        }

        public ArmClient(string defaultSubscription)
        {
            DefaultSubscription = defaultSubscription;
            Subscriptions = new StaticAsyncEnumerable<string>(new[] { defaultSubscription });
        }

        public ArmClient(string defaultSubscription, IEnumerable<string> subscriptionSet)
        {
            DefaultSubscription = defaultSubscription;
            Subscriptions = new StaticAsyncEnumerable<string>(subscriptionSet);
        }
    }
}
