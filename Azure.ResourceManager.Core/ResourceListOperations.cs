// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Core.Resources;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Generic list operations class - can be extended if a specific RP has more list operations
    /// </summary>
    public class ResourceListOperations
    {
        private static void Validate(ResourceIdentifier id)
        {
            if (id.Type != ResourceGroupOperations.AzureResourceType &&
                id.Type != SubscriptionOperations.AzureResourceType)
            {
                throw new ArgumentException(
                    $"{id.Type} is not valid to list at context must be {ResourceGroupOperations.AzureResourceType} or {SubscriptionOperations.AzureResourceType}");
            }
        }

        public static Pageable<TOperations> ListAtContext<TOperations, TResource>(
            ArmClientContext clientContext,
            ArmClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TResource : TrackedResource
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.AzureResourceType ? id.Name : null;

            return _ListAtContext<TOperations, TResource>(
                clientContext,
                clientOptions,
                id,
                scopeId,
                resourceFilters,
                top,
                cancellationToken);
        }

        public static AsyncPageable<TOperations> ListAtContextAsync<TOperations, TResource>(
            ArmClientContext clientContext,
            ArmClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TResource : TrackedResource
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.AzureResourceType ? id.Name : null;

            return _ListAtContextAsync<TOperations, TResource>(
                clientContext,
                clientOptions,
                id,
                scopeId,
                resourceFilters,
                top,
                cancellationToken);
        }

        public static Pageable<TOperations> ListAtContext<TOperations, TResource>(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TResource : TrackedResource
        {
            return _ListAtContext<TOperations, TResource>(
                subscription.ClientContext,
                subscription.ClientOptions,
                subscription.Id,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        public static AsyncPageable<TOperations> ListAtContextAsync<TOperations, TResource>(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TResource : TrackedResource
        {
            return _ListAtContextAsync<TOperations, TResource>(
                subscription.ClientContext,
                subscription.ClientOptions,
                subscription.Id,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        private static AsyncPageable<TOperations> _ListAtContextAsync<TOperations, TResource>(
            ArmClientContext clientContext,
            ArmClientOptions clientOptions,
            ResourceIdentifier scopeId,
            string scopeFilter,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TResource : TrackedResource
        {
            var resourceOperations = GetResourcesClient(clientContext, scopeId.Subscription).Resources;
            AsyncPageable<GenericResourceExpanded> result;
            if (scopeFilter == null)
            {
                result = resourceOperations.ListAsync(resourceFilters?.ToString(), null, top, cancellationToken);
            }
            else
            {
                result = resourceOperations.ListByResourceGroupAsync(
                    scopeFilter,
                    resourceFilters?.ToString(),
                    null,
                    top,
                    cancellationToken);
            }

            return ConvertResultsAsync<TOperations, TResource>(result, clientContext, clientOptions);
        }

        private static Pageable<TOperations> _ListAtContext<TOperations, TResource>(
            ArmClientContext clientContext,
            ArmClientOptions clientOptions,
            ResourceIdentifier scopeId,
            string scopeFilter = null,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TResource : TrackedResource
        {
            var resourceOperations = GetResourcesClient(clientContext, scopeId.Subscription).Resources;
            Pageable<GenericResourceExpanded> result;
            if (scopeFilter == null)
            {
                result = resourceOperations.List(resourceFilters?.ToString(), null, top, cancellationToken);
            }
            else
            {
                result = resourceOperations.ListByResourceGroup(
                    scopeFilter,
                    resourceFilters?.ToString(),
                    null,
                    top,
                    cancellationToken);
            }

            return ConvertResults<TOperations, TResource>(result, clientContext, clientOptions);
        }

        private static Pageable<TOperations> ConvertResults<TOperations, TResource>(
            Pageable<GenericResourceExpanded> result,
            ArmClientContext clientContext,
            ArmClientOptions clientOptions)
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TResource : TrackedResource
        {
            return new PhWrappingPageable<GenericResourceExpanded, TOperations>(
                result,
                CreateResourceConverter<TOperations, TResource>(clientContext, clientOptions));
        }

        private static AsyncPageable<TOperations> ConvertResultsAsync<TOperations, TResource>(
            AsyncPageable<GenericResourceExpanded> result,
            ArmClientContext clientContext,
            ArmClientOptions clientOptions)
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TResource : TrackedResource
        {
            return new PhWrappingAsyncPageable<GenericResourceExpanded, TOperations>(
                result,
                CreateResourceConverter<TOperations, TResource>(clientContext, clientOptions));
        }

        private static Func<GenericResourceExpanded, TOperations> CreateResourceConverter<TOperations, TResource>(ArmClientContext clientContext, ArmClientOptions clientOptions)
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TResource : TrackedResource
        {
             return s => Activator.CreateInstance(
                    typeof(TOperations),
                    clientContext,
                    Activator.CreateInstance(typeof(TResource), s as Azure.ResourceManager.Resources.Models.Resource) as TResource, clientOptions) as TOperations;
        }

        //TODO: should be able to access context.GetClient() instead of needing this method
        protected static ResourcesManagementClient GetResourcesClient(ArmClientContext context, string id)
        {
            return new ResourcesManagementClient(context.BaseUri, id, context.Credential);
        }
    }
}
