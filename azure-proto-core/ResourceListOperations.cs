using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System;
using System.Collections.Generic;
using System.Threading;

namespace azure_proto_core
{
    /// <summary>
    /// Generic list operations class - can be extended if a specific RP has more list operations
    /// TODO: Verify whether Listing by tag works when filtering by resource type
    /// </summary>
    public class ResourceListOperations
    {
        //TODO: this isn't a finalized design for this its a placeholder to expose the functionality for now
        private static Dictionary<Type, Action<ArmFilterCollection, ArmResourceFilter>> _typeSwitch = new Dictionary<Type, Action<ArmFilterCollection, ArmResourceFilter>>()
        {
            { typeof(ArmSubstringFilter), (filterCollection, filter) => { filterCollection.SubstringFilter = (filter as ArmSubstringFilter); } },
            { typeof(ArmTagFilter), (filterCollection, filter) => { filterCollection.TagFilter = (filter as ArmTagFilter); } }
        };

        //TODO: Add overloads to take a set such as ArmFilterCollection
        public static Pageable<U> ListAtContext<U, T>(SubscriptionOperations subscription, ArmResourceFilter resourceFilter = null, int? top = null, CancellationToken cancellationToken = default)
            where U : ResourceOperationsBase<T>
            where T : TrackedResource //TODO: Revisit after we remove Registry to see if we can drop this down to Resource instead of TrackedResource
        {
            return _ListAtContext<U, T>(subscription.ClientContext, subscription.Id, null, resourceFilter, top, cancellationToken);
        }

        public static Pageable<U> ListAtContext<U, T>(ResourceGroupOperations resourceGroup, ArmResourceFilter resourceFilter = null, int? top = null, CancellationToken cancellationToken = default)
            where U : ResourceOperationsBase<T>
            where T : TrackedResource
        {
            return _ListAtContext<U, T>(resourceGroup.ClientContext, resourceGroup.Id, resourceGroup.Id.Name, resourceFilter, top, cancellationToken);
        }

        public static AsyncPageable<U> ListAtContextAsync<U, T>(SubscriptionOperations subscription, ArmResourceFilter resourceFilter = null, int? top = null, CancellationToken cancellationToken = default)
            where U : ResourceOperationsBase<T>
            where T : TrackedResource
        {
            return _ListAtContextAsync<U, T>(subscription.ClientContext, subscription.Id, null, resourceFilter, top, cancellationToken);
        }

        public static AsyncPageable<U> ListAtContextAsync<U, T>(ResourceGroupOperations resourceGroup, ArmResourceFilter resourceFilter = null, int? top = null, CancellationToken cancellationToken = default)
            where U : ResourceOperationsBase<T>
            where T : TrackedResource
        {
            return _ListAtContextAsync<U, T>(resourceGroup.ClientContext, resourceGroup.Id, resourceGroup.Id.Name, resourceFilter, top, cancellationToken);
        }

        private static AsyncPageable<U> _ListAtContextAsync<U, T>(ArmClientContext clientContext, ResourceIdentifier scopeId, string scopeFilter = null, ArmResourceFilter resourceFilter = null, int? top = null, CancellationToken cancellationToken = default)
            where U : ResourceOperationsBase<T>
            where T : TrackedResource
        {
            //TODO: Add the following logic
            //if armfilter != null then use arm list call
            //if armfilter == null && listbysub exists for the rp use rp call
            //else use arm list call
            ResourceType type;
            if (!ArmClient.Registry.TryGetResourceType<U, T>(out type))
                throw new ArgumentException($"{typeof(T)} was not registered");
            var filters = GetFilters(type, resourceFilter);
            var resourceOperations = GetResourcesClient(clientContext, scopeId.Subscription).Resources;
            AsyncPageable<GenericResourceExpanded> result;
            if (scopeFilter == null)
            {
                result = resourceOperations.ListAsync(filters.ToString(), null, top, cancellationToken);
            }
            else
            {
                result = resourceOperations.ListByResourceGroupAsync(scopeFilter, filters.ToString(), null, top, cancellationToken);
            }

            return ConvertResultsAsync<U, T>(result, clientContext);
        }

        private static Pageable<U> _ListAtContext<U, T>(ArmClientContext clientContext, ResourceIdentifier scopeId, string scopeFilter = null, ArmResourceFilter resourceFilter = null, int? top = null, CancellationToken cancellationToken = default)
            where U : ResourceOperationsBase<T>
            where T : TrackedResource
        {
            //TODO: Add the following logic
            //if armfilter != null then use arm list call
            //if armfilter == null && listbysub exists for the rp use rp call
            //else use arm list call

            ResourceType type;
            if (!ArmClient.Registry.TryGetResourceType<U, T>(out type))
                throw new ArgumentException($"{typeof(T)} was not registered");
            var filters = GetFilters(type, resourceFilter);
            var resourceOperations = GetResourcesClient(clientContext, scopeId.Subscription).Resources;
            Pageable<GenericResourceExpanded> result;
            if(scopeFilter == null)
            {
                result = resourceOperations.List(filters.ToString(), null, top, cancellationToken);
            }
            else
            {
                result = resourceOperations.ListByResourceGroup(scopeFilter, filters.ToString(), null, top, cancellationToken);
            }

            return ConvertResults<U, T>(result, clientContext);
        }

        private static Pageable<U> ConvertResults<U, T>(Pageable<GenericResourceExpanded> result, ArmClientContext clientContext)
            where U : ResourceOperationsBase<T>
            where T : TrackedResource
        {
            return new PhWrappingPageable<GenericResourceExpanded, U>(result, s => Activator.CreateInstance(typeof(U), clientContext, new ResourceIdentifier(s.Id)) as U);
        }

        private static AsyncPageable<U> ConvertResultsAsync<U, T>(AsyncPageable<GenericResourceExpanded> result, ArmClientContext clientContext)
            where U : ResourceOperationsBase<T>
            where T : TrackedResource
        {
            return new PhWrappingAsyncPageable<GenericResourceExpanded, U>(result, s => Activator.CreateInstance(typeof(U), clientContext, new ResourceIdentifier(s.Id)) as U);
        }

        private static ArmFilterCollection GetFilters(ResourceType type, ArmResourceFilter resourceFilter)
        {
            var filters = new ArmFilterCollection(type);
            if (resourceFilter != null)
                _typeSwitch[resourceFilter.GetType()](filters, resourceFilter);
            return filters;
        }

        //TODO: should be able to access context.GetClient() instead of needing this method
        protected static ResourcesManagementClient GetResourcesClient(ArmClientContext context, string id)
        {
            return new ResourcesManagementClient(context.BaseUri, id, context.Credential);
        }
    }
}
