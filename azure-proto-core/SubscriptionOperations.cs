using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Subscription operations
    /// TODO: Look at selecting subscriptions by name
    /// </summary>
    public class SubscriptionOperations : OperationsBase
    {
        public ResourceIdentifier Id { get; private set; }
        public SubscriptionOperations(ArmClientContext parent, string defaultSubscription) :base(parent, $"/subscriptions/{defaultSubscription}")
        {
            Id = new ResourceIdentifier($"/subscriptions/{defaultSubscription}");
        }
        public SubscriptionOperations(ArmClientContext parent, ResourceIdentifier defaultSubscription) : base(parent, defaultSubscription)
        {
            Id = defaultSubscription;
        }

        public SubscriptionOperations(ArmClientContext parent, Resource defaultSubscription) : base(parent, defaultSubscription)
        {
            Id = defaultSubscription.Id;
        }

        public ArmOperation<ResourceGroupOperations> CreateResourceGroup(string name, PhResourceGroup resourceDetails)
        {
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(RgOperations.CreateOrUpdate(name, resourceDetails), s => new ResourceGroupOperations(this, new PhResourceGroup(s)));
        }

        public ArmOperation<ResourceGroupOperations> CreateResourceGroup(string name, Location location)
        {
            var model = new PhResourceGroup(new ResourceGroup(location));
            var rgResponse = RgOperations.CreateOrUpdate(name, model);
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(rgResponse, s => new ResourceGroupOperations(this, new PhResourceGroup(s)));
        }

        public async Task<ArmOperation<ResourceGroupOperations>> CreateResourceGroupAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(await RgOperations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken), s => new ResourceGroupOperations(this, new PhResourceGroup(s)));
        }

        public Pageable<ResourceGroupOperations> ListResourceGroups(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingPageable<ResourceGroup, ResourceGroupOperations>(RgOperations.List(null, null, cancellationToken), s => new ResourceGroupOperations(this, new PhResourceGroup(s)));
        }

        public AsyncPageable<ResourceGroupOperations> ListResourceGroupsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingAsyncPageable<ResourceGroup, ResourceGroupOperations>(RgOperations.ListAsync(null, null, cancellationToken), s => new ResourceGroupOperations(this, new PhResourceGroup(s)));
        }

        public Pageable<ResourceOperationsBase<T>> ListResource<T>(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this.ClientContext, Id, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.List(filter, top, cancellationToken);
        }

        public Pageable<ResourceOperationsBase<T>> ListResource<T>(ResourceIdentifier subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this.ClientContext, subscription, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.List(filter, top, cancellationToken);
        }

        public Pageable<ResourceOperationsBase<T>> ListResource<T>(PhSubscriptionModel subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this.ClientContext, subscription.Id, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.List(filter, top, cancellationToken);
        }

        public AsyncPageable<ResourceOperationsBase<T>> ListResourceAsync<T>(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this.ClientContext, Id, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAsync(filter, top, cancellationToken);
        }

        public AsyncPageable<ResourceOperationsBase<T>> ListResourceAsync<T>(ResourceIdentifier resource, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this.ClientContext, resource, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAsync(filter, top, cancellationToken);
        }

        public AsyncPageable<ResourceOperationsBase<T>> ListResourceAsync<T>(PhSubscriptionModel model, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this.ClientContext, model.Id, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAsync(filter, top, cancellationToken);
        }


        public ResourceGroupOperations ResourceGroup(PhResourceGroup resourceGroup)
        {
            return new ResourceGroupOperations(this, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier resourceGroup)
        {
            return new ResourceGroupOperations(this, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(string resourceGroup)
        {
            return new ResourceGroupOperations(this, $"{Id}/resourceGroups/{resourceGroup}");
        }

        public override ResourceType ResourceType => ResourceType.None;

        internal SubscriptionsOperations SubscriptionsClient => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred)).Subscriptions;

        internal ResourceGroupsOperations RgOperations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;
    }
}
