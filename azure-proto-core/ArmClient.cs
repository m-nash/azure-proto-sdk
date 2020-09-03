using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// The entry point for all ARM clients
    /// TODO: What is appropriate naming for ArmClient , given that we would not liek to make distinctions between data and management.
    /// </summary>
    public class ArmClient : ArmOperations
    {
        static ArmClient()
        {
            Registry.Register<PhResourceGroup>(
                new azure_proto_core.Internal.ArmResourceRegistration<PhResourceGroup>(
                    new ResourceType("Microsoft.Resources/resourceGroups"),
                    (o, r) => new ResourceGroupContainerOperations(o, r),
                    null,
                    (o, r) => new ResourceGroupOperations(o, r)));
        }

        public static ArmResourceRegistry Registry { get; }  = new ArmResourceRegistry();
        internal static readonly string DefaultUri = "https://management.azure.com";
        public ArmClient() : this(new Uri(DefaultUri), new DefaultAzureCredential())
        {
        }

        public ArmClient(string subscription) : this(new Uri(DefaultUri), new DefaultAzureCredential(), subscription)
        {
        }

        public ArmClient(TokenCredential credential, string subscription) : this(new Uri(DefaultUri), credential, subscription)
        {
        }

        public ArmClient(Uri baseUri, TokenCredential credential) : base(baseUri, credential)
        {
            DefaultSubscription = GetDefaultSubscription().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public ArmClient(Uri baseUri, TokenCredential credential, string subscription) : base(baseUri, credential)
        {
            DefaultSubscription = subscription;
        }

        public string DefaultSubscription { get; set; }

        public SubscriptionOperations Subscriptions() => new SubscriptionOperations(this, DefaultSubscription);
        public SubscriptionOperations Subscriptions(PhSubscriptionModel subscription) => new SubscriptionOperations(this, subscription);

        /// <summary>
        /// TODO: represent strings that take both resource id or just subscription id
        /// TODO: should we allow subscription friendly names?
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public SubscriptionOperations Subscriptions(ResourceIdentifier subscription) => new SubscriptionOperations(this, subscription);
        public SubscriptionOperations Subscriptions(string subscription) => new SubscriptionOperations(this, $"/subscriptions/{subscription}");

        public AsyncPageable<SubscriptionOperations> ListSubscriptionsAsync(CancellationToken token = default)
        {
            return new PhWrappingAsyncPageable<Subscription, SubscriptionOperations>(SubscriptionsClient.ListAsync(token), s => new SubscriptionOperations(this, new PhSubscriptionModel(s)));
        }

        public Pageable<SubscriptionOperations> ListSubscriptions(CancellationToken token = default)
        {
            return new PhWrappingPageable<Subscription, SubscriptionOperations>(SubscriptionsClient.List(token), s => new SubscriptionOperations(this, new PhSubscriptionModel(s)));
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

                return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.Location, PhLocation>(SubscriptionsClient.ListLocationsAsync(subscriptionId, token), s => new PhLocation(s));

            }

            return new PhTaskDeferringAsyncPageable<PhLocation>(PageableFunc);

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

            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.Location, PhLocation>(SubscriptionsClient.ListLocations(subscriptionId, cancellationToken), s => new PhLocation(s));
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

        public virtual IEnumerable<azure_proto_core.Location> ListAvailableLocations<T>(CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, $"/subscriptions/{DefaultSubscription}", out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAvailableLocations(cancellationToken);
        }

        public virtual IEnumerable<azure_proto_core.Location> ListAvailableLocations<T>(PhSubscriptionModel subscription, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, subscription.Id, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAvailableLocations(cancellationToken);
        }

        public virtual IEnumerable<azure_proto_core.Location> ListAvailableLocations<T>(ResourceIdentifier subscription, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, subscription, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAvailableLocations(cancellationToken);
        }

        public virtual IEnumerable<azure_proto_core.Location> ListAvailableLocations<T>(string subscription, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, $"/subscriptions/{subscription}", out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAvailableLocations(cancellationToken);
        }



        public ResourceGroupOperations ResourceGroup(string subscription, string resourceGroup)
        {
            return new ResourceGroupOperations(this, $"/subscriptions/{subscription}/resourceGroups/{resourceGroup}");
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier resourceGroup)
        {
            return new ResourceGroupOperations(this, resourceGroup);
        }
        public ResourceGroupOperations ResourceGroup(PhResourceGroup resourceGroup)
        {
            return new ResourceGroupOperations(this, resourceGroup);
        }

        public Pageable<ResourceOperations<T>> ListResource<T>(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, $"/subscriptions/{DefaultSubscription}", out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.List(filter, top, cancellationToken);
        }

        public Pageable<ResourceOperations<T>> ListResource<T>(ResourceIdentifier subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, subscription, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.List(filter, top, cancellationToken);
        }

        public Pageable<ResourceOperations<T>> ListResource<T>(PhSubscriptionModel subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, subscription.Id, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.List(filter, top, cancellationToken);
        }

        public AsyncPageable<ResourceOperations<T>> ListResourceAsync<T>(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, $"/subscriptions/{DefaultSubscription}", out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAsync(filter, top, cancellationToken);
        }

        public AsyncPageable<ResourceOperations<T>> ListResourceAsync<T>(ResourceIdentifier resource, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, resource, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAsync(filter, top, cancellationToken);
        }

        public AsyncPageable<ResourceOperations<T>> ListResourceAsync<T>(PhSubscriptionModel model, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!Registry.TryGetColletcion<T>(this, model.Id, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAsync(filter, top, cancellationToken);
        }

        public ResourceOperations<T> GetResourceOperations<T>(TrackedResource resource) where T : TrackedResource
        {
            ResourceOperations<T> operations;
            if (!Registry.TryGetOperations<T>(this, resource, out operations))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return operations;
        }

        public ResourceOperations<T> GetResourceOperations<T>(ResourceIdentifier resource) where T : TrackedResource
        {
            var placeholder = new ArmResource(resource);
            ResourceOperations<T> operations;
            if (!Registry.TryGetOperations<T>(this, placeholder, out operations))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return operations;
        }

        public ResourceOperations<T> GetResourceOperations<T>(string subscription, string resourceGroup, string name) where T : TrackedResource
        {
            ResourceType type;
            if (!Registry.TryGetResourceType<T>(out type))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            var id = new ResourceIdentifier($"/subscriptions/{subscription}/resourceGroups/{resourceGroup}/providers/{type.Namespace}/{type.Type}/name");
            return GetResourceOperations<T>(id);
        }


        public ArmOperation<ResourceOperations<T>> CreateResource<T>(string subscription, string resourceGroup, string name, T model, azure_proto_core.Location location = default) where T:TrackedResource
        {
            if (location == null)
            {
                location = azure_proto_core.Location.Default;
            }

            ResourceContainerOperations<T> container;
            if (!Registry.TryGetContainer<T>(this, new ArmResource($"/subscriptions/{subscription}/resourceGroups/{resourceGroup}", location), out container))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return container.Create(name, model);
        }


        /// <summary>
        /// Fill in the default subscription in the simple case (passed in, or only one subscription available)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        internal async Task<string> GetDefaultSubscription(CancellationToken token = default(CancellationToken))
        {
            string sub = DefaultSubscription;
            if (null == sub)
            {
                var subs = ListSubscriptionsAsync(token).GetAsyncEnumerator();
                if (await subs.MoveNextAsync())
                {
                    PhSubscriptionModel localSub;
                    if (subs.Current != null && subs.Current.TryGetModel(out localSub))
                    {
                        sub = localSub?.Id.Subscription;
                    }
                }
            }

            return sub;
        }

        internal SubscriptionsOperations SubscriptionsClient => GetResourcesClient(Guid.NewGuid().ToString()).Subscriptions;

        protected override ResourceType ResourceType => ResourceType.None;

        internal ResourcesManagementClient GetResourcesClient(string subscription) => GetClient<ResourcesManagementClient>((uri, credential) => new ResourcesManagementClient(uri, subscription, credential));

    }
}
