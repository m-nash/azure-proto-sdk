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
    ///  Generic list operations class. This can be extended if a specific RP has more list operations.
    /// </summary>
    public class ResourceListOperations
    {
        /// <summary>
        /// List resources under the a resource context
        /// </summary>
        /// <typeparam name="TOperations">The type of the class containing operations for the underlying resource</typeparam>
        /// <typeparam name="TResource">The type of the class containing properties for the underlying resource</typeparam>
        /// <param name="clientOptions">The client parameters to use in these operations.</param>
        /// <param name="id">The identifier of the resource that is the target of operations.</param>
        /// <param name="resourceFilters">Optional filters for results</param>
        /// <param name="top">The number of results to return.</param>
        /// <param name="cancellationToken">A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
        /// <returns>A collection of resource operations that may take multiple service requests to iterate over.</returns>
        /// <exception cref="ArgumentException"><paramref name="id"/> is not valid to list at context.</exception>
        public static Pageable<TOperations> ListAtContext<TOperations, TResource>(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations>
            where TResource : TrackedResource
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.ResourceType ? id.Name : null;

            return ListAtContextInternal<TOperations, TResource>(
                clientOptions,
                id,
                scopeId,
                resourceFilters,
                top,
                cancellationToken);
        }

        /// <summary>
        /// List resources under the a resource context
        /// </summary>
        /// <typeparam name="TOperations">The type of the class containing operations for the underlying resource</typeparam>
        /// <typeparam name="TResource">The type of the class containing properties for the underlying resource</typeparam>
        /// <param name="clientOptions">The client parameters to use in these operations.</param>
        /// <param name="id">The identifier of the resource that is the target of operations.</param>
        /// <param name="resourceFilters">Optional filters for results</param>
        /// <param name="top">The number of results to return.</param>
        /// <param name="cancellationToken">A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
        /// <returns>An async collection of resource operations that may take multiple service requests to iterate over.</returns>
        /// <exception cref="ArgumentException"><paramref name="id"/> is not valid to list at context.</exception>
        public static AsyncPageable<TOperations> ListAtContextAsync<TOperations, TResource>(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations>
            where TResource : TrackedResource
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.ResourceType ? id.Name : null;

            return ListAtContextInternalAsync<TOperations, TResource>(
                clientOptions,
                id,
                scopeId,
                resourceFilters,
                top,
                cancellationToken);
        }

        /// <summary>
        /// List resources under a subscription
        /// </summary>
        /// <typeparam name="TOperations">The type of the class containing operations for the underlying resource</typeparam>
        /// <typeparam name="TResource">The type of the class containing properties for the underlying resource</typeparam>
        /// <param name="subscription">The id of the Azure subscription.</param>
        /// <param name="resourceFilters">Optional filters for results</param>
        /// <param name="top">The number of results to return.</param>
        /// <param name="cancellationToken">A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
        /// <returns>A collection of resource operations that may take multiple service requests to iterate over.</returns>
        public static Pageable<TOperations> ListAtContext<TOperations, TResource>(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations>
            where TResource : TrackedResource
        {
            return ListAtContextInternal<TOperations, TResource>(
                subscription.ClientOptions,
                subscription.Id,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        /// <summary>
        /// List resources under the a resource context
        /// </summary>
        /// <typeparam name="TOperations">The type of the class containing operations for the underlying resource</typeparam>
        /// <typeparam name="TResource">The type of the class containing properties for the underlying resource</typeparam>
        /// <param name="subscription">The id of the Azure subscription.</param>
        /// <param name="resourceFilters">Optional filters for results</param>
        /// <param name="top">The number of results to return.</param>
        /// <param name="cancellationToken">A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
        /// <returns>An async collection of resource operations that may take multiple service requests to iterate over.</returns>
        public static AsyncPageable<TOperations> ListAtContextAsync<TOperations, TResource>(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations>
            where TResource : TrackedResource
        {
            return ListAtContextInternalAsync<TOperations, TResource>(
                subscription.ClientOptions,
                subscription.Id,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        private static ResourcesManagementClient GetResourcesClient(AzureResourceManagerClientOptions options, string id)
        {
            // TODO: should be able to access options.GetClient() instead of needing this method
            return new ResourcesManagementClient(options.BaseUri, id, options.Credential);
        }

        private static AsyncPageable<TOperations> ListAtContextInternalAsync<TOperations, TResource>(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier scopeId,
            string scopeFilter,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations>
            where TResource : TrackedResource
        {
            var resourceOperations = GetResourcesClient(clientOptions, scopeId.Subscription).Resources;
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

            return ConvertResultsAsync<TOperations, TResource>(result, clientOptions);
        }

        private static Pageable<TOperations> ListAtContextInternal<TOperations, TResource>(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier scopeId,
            string scopeFilter = null,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where TOperations : ResourceOperationsBase<TOperations>
            where TResource : TrackedResource
        {
            var resourceOperations = GetResourcesClient(clientOptions, scopeId.Subscription).Resources;
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

            return ConvertResults<TOperations, TResource>(result, clientOptions);
        }

        private static Pageable<TOperations> ConvertResults<TOperations, TResource>(
            Pageable<GenericResourceExpanded> result,
            AzureResourceManagerClientOptions clientOptions)
            where TOperations : ResourceOperationsBase<TOperations>
            where TResource : TrackedResource
        {
            return new PhWrappingPageable<GenericResourceExpanded, TOperations>(
                result,
                CreateResourceConverter<TOperations, TResource>(clientOptions));
        }

        private static AsyncPageable<TOperations> ConvertResultsAsync<TOperations, TResource>(
            AsyncPageable<GenericResourceExpanded> result,
            AzureResourceManagerClientOptions clientOptions)
            where TOperations : ResourceOperationsBase<TOperations>
            where TResource : TrackedResource
        {
            return new PhWrappingAsyncPageable<GenericResourceExpanded, TOperations>(
                result,
                CreateResourceConverter<TOperations, TResource>(clientOptions));
        }

        private static Func<GenericResourceExpanded, TOperations> CreateResourceConverter<TOperations, TResource>(
            AzureResourceManagerClientOptions clientOptions)
            where TOperations : ResourceOperationsBase<TOperations>
            where TResource : TrackedResource
        {
             return s => Activator.CreateInstance(
                    typeof(TOperations),
                    clientOptions,
                    Activator.CreateInstance(typeof(TResource), s as GenericResource) as TResource) as TOperations;
        }

        private static void Validate(ResourceIdentifier id)
        {
            if (id.Type != ResourceGroupOperations.ResourceType &&
                id.Type != SubscriptionOperations.ResourceType)
            {
                throw new ArgumentException(
                    $"{id.Type} is not valid to list at context must be {ResourceGroupOperations.ResourceType} or {SubscriptionOperations.ResourceType}");
            }
        }
    }
}
