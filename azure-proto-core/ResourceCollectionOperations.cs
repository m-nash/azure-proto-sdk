using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
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
    /// Generic list operations class - can be extended if a specific RP has more list operations
    /// TODO: Verify whether Listing by tag works when filtering by resource type
    /// </summary>
    public abstract class ResourceCollectionOperations<T> : ResourceOperationsBase where T : TrackedResource
    {
        public ResourceCollectionOperations(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public ResourceCollectionOperations(ArmClientBase parent, Resource context) : this(parent, context?.Id)
        {
        }

        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier?.Type != ResourceType)
            {
                throw new InvalidOperationException($"{identifier} is not a valid resource of type '{ResourceType}'");
            }
        }

        public virtual Pageable<ResourceClientBase<T>> List(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var innerFilter = new ArmFilterCollection(ResourceType);
            innerFilter.SubstringFilter = filter;
            return ListAtContext(Context, innerFilter, top, cancellationToken);
        }

        public virtual AsyncPageable<ResourceClientBase<T>> ListAsync(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var innerFilter = new ArmFilterCollection(ResourceType);
            innerFilter.SubstringFilter = filter;
            return ListAtContextAsync(Context, innerFilter, top, cancellationToken);
        }

        public virtual Pageable<ResourceClientBase<T>> ListByTag(ArmTagFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var innerFilter = new ArmFilterCollection(ResourceType);
            innerFilter.TagFilter = filter;
            return ListAtContext(Context, innerFilter, top, cancellationToken);
        }

        public virtual AsyncPageable<ResourceClientBase<T>> ListByTagAsync(ArmTagFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var innerFilter = new ArmFilterCollection(ResourceType);
            innerFilter.TagFilter = filter;
            return ListAtContextAsync(Context, innerFilter, top, cancellationToken);
        }

        public virtual IEnumerable<Location> ListAvailableLocations(CancellationToken cancellationToken = default)
        {
            return Providers.List(expand: "metadata", cancellationToken: cancellationToken)
                .FirstOrDefault(p => string.Equals(p.Namespace, ResourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                .ResourceTypes.FirstOrDefault(r => ResourceType.Equals(r.ResourceType))
                .Locations.Select(l => new Location(l));
        }

        public async virtual IAsyncEnumerable<Location> ListAvailableLocationsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var provider in Providers.ListAsync(expand: "metadata", cancellationToken: cancellationToken).WithCancellation(cancellationToken))
            {
                if (string.Equals(provider.Namespace, ResourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                {
                    var foundResource = provider.ResourceTypes.FirstOrDefault(p => ResourceType.Equals(p.ResourceType));
                    foreach (var location in foundResource.Locations)
                    {
                        yield return new Location(location);
                    }
                }
            }
        }



        protected virtual Pageable<ResourceClientBase<T>> ListAtContext(ResourceIdentifier context, ArmFilterCollection filters, int? top, CancellationToken cancellationToken = default)
        {
            Pageable<GenericResourceExpanded> result;
            if (context?.Type == "Microsoft.Resources/resourceGroups")
            {
                result = GetResourcesClient(context.Subscription)
                  .Resources.ListByResourceGroup(context.Name, filters.ToString(), null, top, cancellationToken);
            }
            else if (context?.Type == "Microsoft.Resources/subscriptions")
            {
                result = GetResourcesClient(context.Subscription)
                 .Resources.List(filters.ToString(), null, top, cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("Invalid context: {subscription}");
            }

            return new PhWrappingPageable<GenericResourceExpanded, ResourceClientBase<T>>(result, s => GetOperations(s.Id, s.Location));
        }

        protected virtual AsyncPageable<ResourceClientBase<T>> ListAtContextAsync(ResourceIdentifier context, ArmFilterCollection filters, int? top, CancellationToken cancellationToken = default)
        {
            AsyncPageable<GenericResourceExpanded> result;
            if (context?.Type == "Microsoft.Resources/resourceGroups")
            {
                result = GetResourcesClient(context.Subscription)
                  .Resources.ListByResourceGroupAsync(context.Name, filters.ToString(), null, top, cancellationToken);
            }
            else if (context?.Type == "Microsoft.Resources/subscriptions")
            {
                result = GetResourcesClient(context.Subscription)
                 .Resources.ListAsync(filters.ToString(), null, top, cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("Invalid context: {subscription}");
            }

            return new PhWrappingAsyncPageable<GenericResourceExpanded, ResourceClientBase<T>>(result, s => GetOperations(s.Id, s.Location));
        }

        protected abstract ResourceClientBase<T> GetOperations(ResourceIdentifier identifier, Location location);

        protected ResourcesManagementClient GetResourcesClient(string subscription)
        {
            return GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, subscription, cred));
        }

        protected ProvidersOperations Providers => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Context.Subscription, cred)).Providers;

    }
}
