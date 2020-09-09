using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System;
using System.Threading;

namespace azure_proto_core
{
    /// <summary>
    /// Generic list operations class - can be extended if a specific RP has more list operations
    /// TODO: Verify whether Listing by tag works when filtering by resource type
    /// </summary>
    public class ResourceListOperations
    {
        public static Pageable<U> ListAtContext<U, T>(SubscriptionOperations subscription, ArmSubstringFilter substringFilter, int? top, Func<string, string, U> ctor, CancellationToken cancellationToken = default)
            where U : ResourceClientBase<T>
            where T : Resource
        {
            var filters = GetFilters(subscription.Id, substringFilter);
            var result = GetResourcesClient(subscription, subscription.Id.Subscription).Resources.List(filters.ToString(), null, top, cancellationToken);

            return ConvertResults<U, T>(ctor, result);
        }

        public static Pageable<U> ListAtContext<U, T>(ResourceGroupOperations resourceGroup, ArmSubstringFilter substringFilter, int? top, Func<string, string, U> ctor, CancellationToken cancellationToken = default)
            where U : ResourceClientBase<T>
            where T : Resource
        {
            var filters = GetFilters(resourceGroup.Context, substringFilter);
            var result = GetResourcesClient(resourceGroup, resourceGroup.Context.Subscription).Resources.ListByResourceGroup(resourceGroup.Context.Name, filters.ToString(), null, top, cancellationToken);

            return ConvertResults<U, T>(ctor, result);
        }

        public static AsyncPageable<U> ListAtContextAsync<U, T>(SubscriptionOperations subscription, ArmSubstringFilter substringFilter, int? top, Func<string, string, U> ctor, CancellationToken cancellationToken = default)
            where U : ResourceClientBase<T>
            where T : Resource
        {
            var filters = GetFilters(subscription.Id, substringFilter);
            var result = GetResourcesClient(subscription, subscription.Id.Subscription).Resources.ListAsync(filters.ToString(), null, top, cancellationToken);

            return ConvertResultsAsync<U, T>(ctor, result);
        }

        public static AsyncPageable<U> ListAtContextAsync<U, T>(ResourceGroupOperations resourceGroup, ArmSubstringFilter substringFilter, int? top, Func<string, string, U> ctor, CancellationToken cancellationToken = default)
            where U : ResourceClientBase<T>
            where T : Resource
        {
            var filters = GetFilters(resourceGroup.Context, substringFilter);
            var result = GetResourcesClient(resourceGroup, resourceGroup.Context.Subscription).Resources.ListByResourceGroupAsync(resourceGroup.Context.Name, filters.ToString(), null, top, cancellationToken);

            return ConvertResultsAsync<U, T>(ctor, result);
        }

        private static Pageable<U> ConvertResults<U, T>(Func<string, string, U> ctor, Pageable<GenericResourceExpanded> result)
            where U : ResourceClientBase<T>
            where T : Resource
        {
            return new PhWrappingPageable<GenericResourceExpanded, U>(result, s => ctor(s.Id, s.Location));
        }

        private static AsyncPageable<U> ConvertResultsAsync<U, T>(Func<string, string, U> ctor, AsyncPageable<GenericResourceExpanded> result)
            where U : ResourceClientBase<T>
            where T : Resource
        {
            return new PhWrappingAsyncPageable<GenericResourceExpanded, U>(result, s => ctor(s.Id, s.Location));
        }

        private static object GetFilters(ResourceIdentifier id, ArmSubstringFilter substringFilter)
        {
            var filters = new ArmFilterCollection(id.Type);
            filters.SubstringFilter = substringFilter;
            return filters;
        }

        protected static ResourcesManagementClient GetResourcesClient(ArmClientBase context, string subscription)
        {
            return new ResourcesManagementClient(context.BaseUri, subscription, context.Credential);
        }
    }
}
