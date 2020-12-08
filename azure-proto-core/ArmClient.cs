// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;

    /// <summary>
    ///     The entry point for all ARM clients.  Note that, we may not want to take a dirrect dependency on Azure.Identity, so
    ///     we may make the
    ///     credential required.
    ///     TODO: What is appropriate naming for ArmClient , given that we would not liek to make distinctions between data and
    ///     management.
    /// </summary>
    public class ArmClient
    {
        internal static readonly string DefaultUri = "https://management.azure.com";

        public ArmClient()
            : this(new Uri(DefaultUri), new DefaultAzureCredential(), null, new ArmClientOptions())
        {
        }

        public ArmClient(string defaultSubscriptionId)
            : this(new Uri(DefaultUri), new DefaultAzureCredential(), defaultSubscriptionId, new ArmClientOptions())
        {
        }

        public ArmClient(TokenCredential credential, string defaultSubscriptionId)
            : this(new Uri(DefaultUri), credential, defaultSubscriptionId, new ArmClientOptions())
        {
        }

        public ArmClient(Uri baseUri, TokenCredential credential)
            : this(baseUri, credential, null, new ArmClientOptions())
        {
        }

        public ArmClient(
            Uri baseUri,
            TokenCredential credential,
            string defaultSubscriptionId,
            ArmClientOptions options)
        {
            ClientContext = new ArmClientContext(baseUri, credential);
            defaultSubscriptionId ??= GetDefaultSubscription().ConfigureAwait(false).GetAwaiter().GetResult();
            DefaultSubscription = new SubscriptionOperations(
                ClientContext,
                new ResourceIdentifier($"/subscriptions/{defaultSubscriptionId}"));
            ApiVersionOverrides = new Dictionary<string, string>();
            ClientOptions = options;
        }

        public Dictionary<string, string> ApiVersionOverrides { get; }

        public ArmClientOptions ClientOptions { get; }

        public SubscriptionOperations DefaultSubscription { get; }

        internal virtual ArmClientContext ClientContext { get; }

        internal SubscriptionsOperations SubscriptionsClient =>
            GetResourcesClient(Guid.NewGuid().ToString()).Subscriptions;

        public SubscriptionOperations Subscription(SubscriptionData subscription)
        {
            return new SubscriptionOperations(ClientContext, subscription);
        }

        /// <summary>
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public SubscriptionOperations Subscription(ResourceIdentifier subscription)
        {
            return new SubscriptionOperations(ClientContext, subscription);
        }

        public SubscriptionOperations Subscription(string subscription)
        {
            return new SubscriptionOperations(ClientContext, subscription);
        }

        public AsyncPageable<SubscriptionOperations> ListSubscriptionsAsync(CancellationToken token = default)
        {
            return new PhWrappingAsyncPageable<Subscription, SubscriptionOperations>(
                SubscriptionsClient.ListAsync(token),
                s => new SubscriptionOperations(ClientContext, new SubscriptionData(s)));
        }

        public Pageable<SubscriptionOperations> ListSubscriptions(CancellationToken token = default)
        {
            return new PhWrappingPageable<Subscription, SubscriptionOperations>(
                SubscriptionsClient.List(token),
                s => new SubscriptionOperations(ClientContext, new SubscriptionData(s)));
        }

        public AsyncPageable<LocationData> ListLocationsAsync(
            string subscriptionId = null,
            CancellationToken token = default)
        {
            async Task<AsyncPageable<LocationData>> PageableFunc()
            {
                if (string.IsNullOrWhiteSpace(subscriptionId))
                {
                    subscriptionId = await GetDefaultSubscription(token);

                    if (subscriptionId == null)
                    {
                        throw new InvalidOperationException("Please select a default subscription");
                    }
                }

                return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.Location, LocationData>(
                    SubscriptionsClient.ListLocationsAsync(subscriptionId, token),
                    s => new LocationData(s));
            }

            return new PhTaskDeferringAsyncPageable<LocationData>(PageableFunc);
        }

        public Pageable<LocationData> ListLocations(
            string subscriptionId = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                subscriptionId = GetDefaultSubscription(cancellationToken).ConfigureAwait(false).GetAwaiter()
                    .GetResult();

                if (subscriptionId == null)
                {
                    throw new InvalidOperationException("Please select a default subscription");
                }
            }

            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.Location, LocationData>(
                SubscriptionsClient.ListLocations(subscriptionId, cancellationToken),
                s => new LocationData(s));
        }

        public async IAsyncEnumerable<Location> ListAvailableLocationsAsync(
            ResourceType resourceType,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var subscriptionId = await GetDefaultSubscription(cancellationToken);

            if (subscriptionId == null)
            {
                throw new InvalidOperationException("Please select a default subscription");
            }

            await foreach (var location in ListAvailableLocationsAsync(subscriptionId, resourceType, cancellationToken))
            {
                yield return location;
            }
        }

        public async IAsyncEnumerable<Location> ListAvailableLocationsAsync(
            string subscription,
            ResourceType resourceType,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var provider in GetResourcesClient(subscription).Providers
                .ListAsync(expand: "metadata", cancellationToken: cancellationToken)
                .WithCancellation(cancellationToken))
            {
                if (string.Equals(
                    provider.Namespace,
                    resourceType?.Namespace,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    var foundResource = provider.ResourceTypes.FirstOrDefault(p => resourceType.Equals(p.ResourceType));

                    foreach (var location in foundResource.Locations)
                    {
                        yield return new Location(location);
                    }
                }
            }
        }

        public IEnumerable<Location> ListAvailableLocations(
            ResourceType resourceType,
            CancellationToken cancellationToken = default)
        {
            var subscription = GetDefaultSubscription().ConfigureAwait(false).GetAwaiter().GetResult();

            return ListAvailableLocations(subscription, resourceType, cancellationToken);
        }

        public IEnumerable<Location> ListAvailableLocations(
            string subscription,
            ResourceType resourceType,
            CancellationToken cancellationToken = default)
        {
            return GetResourcesClient(subscription).Providers
                .List(expand: "metadata", cancellationToken: cancellationToken)
                .FirstOrDefault(
                    p =>
                        string.Equals(
                            p.Namespace,
                            resourceType?.Namespace,
                            StringComparison.InvariantCultureIgnoreCase))
                .ResourceTypes.FirstOrDefault(r => resourceType.Equals(r.ResourceType))
                .Locations.Select(l => new Location(l));
        }

        public ResourceGroupOperations ResourceGroup(string subscription, string resourceGroup)
        {
            return new ResourceGroupOperations(
                ClientContext,
                $"/subscriptions/{subscription}/resourceGroups/{resourceGroup}");
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(ResourceGroupData resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, resourceGroup.Id);
        }

        public T GetResourceOperationsBase<T>(TrackedResource resource)
            where T : TrackedResource
        {
            return Activator.CreateInstance(typeof(T), ClientContext, resource) as T;
        }

        public T GetResourceOperationsBase<T>(ResourceIdentifier resource)
            where T : OperationsBase
        {
            return Activator.CreateInstance(typeof(T), ClientContext, resource) as T;
        }

        public T GetResourceOperationsBase<T>(string subscription, string resourceGroup, string name)
            where T : OperationsBase
        {
            return null;
        }

        public ArmResponse<TOperations> CreateResource<TContainer, TOperations, TResource>(
            string subscription,
            string resourceGroup,
            string name,
            TResource model,
            Location location = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
        {
            if (location == null)
            {
                location = Location.Default;
            }

            var container = Activator.CreateInstance(
                    typeof(TContainer),
                    ClientContext,
                    new ArmResource($"/subscriptions/{subscription}/resourceGroups/{resourceGroup}", location)) as
                TContainer;

            return container.Create(name, model);
        }

        /// <summary>
        ///     Fill in the default subscription in the simple case (passed in, or only one subscription available)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        internal async Task<string> GetDefaultSubscription(CancellationToken token = default)
        {
            var sub = DefaultSubscription?.Id?.Subscription;
            if (null == sub)
            {
                var subs = ListSubscriptionsAsync(token).GetAsyncEnumerator();
                if (await subs.MoveNextAsync())
                {
                    if (subs.Current != null)
                    {
                        sub = subs.Current.Id.Subscription;
                    }
                }
            }

            return sub;
        }

        internal ResourcesManagementClient GetResourcesClient(string subscription)
        {
            return ClientContext.GetClient(
                (uri, credential) =>
                    new ResourcesManagementClient(uri, subscription, credential));
        }
    }
}
