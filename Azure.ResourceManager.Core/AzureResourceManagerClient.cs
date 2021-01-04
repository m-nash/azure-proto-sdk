using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// The entry point for all ARM clients.  Note that, we may not want to take a dirrect dependency on Azure.Identity, so we may make the
    /// credential required.
    /// TODO: What is appropriate naming for ArmClient , given that we would not liek to make distinctions between data and management.
    /// </summary>
    public class AzureResourceManagerClient
    {
        internal static readonly string DefaultUri = "https://management.azure.com";

        public Dictionary<string, string> ApiVersionOverrides { get; private set; }


        public AzureResourceManagerClient()
            : this(new Uri(DefaultUri), new DefaultAzureCredential(), null, null) { }
        public AzureResourceManagerClient(AzureResourceManagerClientOptions options)
            : this(options.BaseUri, options.Credential, null, options) { }
        public AzureResourceManagerClient(string defaultSubscriptionId)
            : this(new Uri(DefaultUri), new DefaultAzureCredential(), defaultSubscriptionId, null) { }

        public AzureResourceManagerClient(string defaultSubscriptionId, AzureResourceManagerClientOptions options)
            : this(options.BaseUri, options.Credential, defaultSubscriptionId, null) { }

        public AzureResourceManagerClient(TokenCredential credential, string defaultSubscriptionId)
            : this(new Uri(DefaultUri), credential, defaultSubscriptionId, null) { }

        public AzureResourceManagerClient(TokenCredential credential, string defaultSubscriptionId, AzureResourceManagerClientOptions options)
            : this(options.BaseUri, credential, defaultSubscriptionId, options) { }

        public AzureResourceManagerClient(Uri baseUri, TokenCredential credential, AzureResourceManagerClientOptions options = null)
            : this(baseUri, credential, null, options) { }

        public AzureResourceManagerClient(Uri baseUri, TokenCredential credential, string defaultSubscriptionId, AzureResourceManagerClientOptions options)
        {
            ClientOptions = new AzureResourceManagerClientOptions(baseUri, credential, options);
            defaultSubscriptionId ??= GetDefaultSubscription().ConfigureAwait(false).GetAwaiter().GetResult();
            DefaultSubscription = new SubscriptionOperations(ClientOptions, new ResourceIdentifier($"/subscriptions/{defaultSubscriptionId}"));
            ApiVersionOverrides = new Dictionary<string, string>();
        }

        public SubscriptionOperations DefaultSubscription { get; private set; }

        internal virtual AzureResourceManagerClientOptions ClientOptions { get; }

        public SubscriptionOperations Subscription(SubscriptionData subscription) => new SubscriptionOperations(ClientOptions, subscription);

        /// <summary>
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public SubscriptionOperations Subscription(ResourceIdentifier subscription) => new SubscriptionOperations(ClientOptions, subscription);

        public SubscriptionOperations Subscription(string subscription) => new SubscriptionOperations(ClientOptions, subscription);

        public SubscriptionContainer Subscriptions()
        {
            return new SubscriptionContainer(ClientOptions);
        }

        public AsyncPageable<LocationData> ListLocationsAsync(string subscriptionId = null, CancellationToken token = default(CancellationToken))
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

                return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.Location, LocationData>(SubscriptionsClient.ListLocationsAsync(subscriptionId, token), s => new LocationData(s));

            }

            return new PhTaskDeferringAsyncPageable<LocationData>(PageableFunc);

        }

        public Pageable<LocationData> ListLocations(string subscriptionId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                subscriptionId = GetDefaultSubscription(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult();
                if (subscriptionId == null)
                {
                    throw new InvalidOperationException("Please select a default subscription");
                }
            }

            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.Location, LocationData>(SubscriptionsClient.ListLocations(subscriptionId, cancellationToken), s => new LocationData(s));
        }

        public async IAsyncEnumerable<Location> ListAvailableLocationsAsync(ResourceType resourceType, [EnumeratorCancellation] CancellationToken cancellationToken = default)
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

        public async IAsyncEnumerable<Location> ListAvailableLocationsAsync(string subscription, ResourceType resourceType, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var provider in GetResourcesClient(subscription).Providers.ListAsync(expand: "metadata", cancellationToken: cancellationToken).WithCancellation(cancellationToken))
            {
                if (string.Equals(provider.Namespace, resourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                {
                    var foundResource = provider.ResourceTypes.FirstOrDefault(p => resourceType.Equals(p.ResourceType));
                    foreach (var location in foundResource.Locations)
                    {
                        yield return location;
                    }
                }
            }
        }

        public IEnumerable<Location> ListAvailableLocations(ResourceType resourceType, CancellationToken cancellationToken = default(CancellationToken))
        {
            var subscription = GetDefaultSubscription().ConfigureAwait(false).GetAwaiter().GetResult();
            return ListAvailableLocations(subscription, resourceType, cancellationToken);
        }

        public IEnumerable<Location> ListAvailableLocations(string subscription, ResourceType resourceType, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetResourcesClient(subscription).Providers.List(expand: "metadata", cancellationToken: cancellationToken)
                .FirstOrDefault(p => string.Equals(p.Namespace, resourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                .ResourceTypes.FirstOrDefault(r => resourceType.Equals(r.ResourceType))
                .Locations.Cast<Location>();
        }

        public ResourceGroupOperations ResourceGroup(string subscription, string resourceGroup)
        {
            return new ResourceGroupOperations(ClientOptions, $"/subscriptions/{subscription}/resourceGroups/{resourceGroup}");
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier resourceGroup)
        {
            return new ResourceGroupOperations(ClientOptions, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(ResourceGroupData resourceGroup)
        {
            return new ResourceGroupOperations(ClientOptions, resourceGroup.Id);
        }

        public T GetResourceOperationsBase<T>(TrackedResource resource) where T : TrackedResource
        {
            return Activator.CreateInstance(typeof(T), ClientOptions, resource) as T;
        }

        public T GetResourceOperationsBase<T>(ResourceIdentifier resource) where T : OperationsBase
        {
            return Activator.CreateInstance(typeof(T), ClientOptions, resource) as T;
        }

        public T GetResourceOperationsBase<T>(string subscription, string resourceGroup, string name) where T : OperationsBase
        {
            return null;
        }

        public ArmResponse<TOperations> CreateResource<TContainer, TOperations, TResource>(string subscription, string resourceGroup, string name, TResource model, Location location = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations>
            where TContainer : ResourceContainerBase<TOperations, TResource>
        {
            if (location == null)
            {
                location = Location.Default;
            }

            TContainer container = Activator.CreateInstance(typeof(TContainer), ClientOptions, new ArmResourceData($"/subscriptions/{subscription}/resourceGroups/{resourceGroup}", location)) as TContainer;

            return container.Create(name, model);
        }

        /// <summary>
        /// Fill in the default subscription in the simple case (passed in, or only one subscription available)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        internal async Task<string> GetDefaultSubscription(CancellationToken token = default(CancellationToken))
        {
            string sub = DefaultSubscription?.Id?.Subscription;
            if (null == sub)
            {
                sub = await this.Subscriptions().GetDefaultSubscription();
            }
            return sub;
        }

        internal SubscriptionsOperations SubscriptionsClient => GetResourcesClient(Guid.NewGuid().ToString()).Subscriptions;

        internal ResourcesManagementClient GetResourcesClient(string subscription) => ClientOptions.GetClient((uri, credential) => new ResourcesManagementClient(uri, subscription, credential));
    }
}