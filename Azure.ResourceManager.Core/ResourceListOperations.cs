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
        /// <param name="clientOptions"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        /// <param name="resourceFilters"> Optional filters for results. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of resource operations that may take multiple service requests to iterate over. </returns>
        /// <exception cref="ArgumentException"> <paramref name="id"/> is not valid to list at context. </exception>
        public static Pageable<ArmResource> ListAtContext(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.ResourceType ? id.Name : null;

            return ListAtContextInternal(
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
        /// <param name="clientOptions"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        /// <param name="resourceFilters"> Optional filters for results. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns>An async collection of resource operations that may take multiple service requests to iterate over. </returns>
        /// <exception cref="ArgumentException"> <paramref name="id"/> is not valid to list at context. </exception>
        public static AsyncPageable<ArmResource> ListAtContextAsync(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.ResourceType ? id.Name : null;

            return ListAtContextInternalAsync(
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
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <param name="resourceFilters"> Optional filters for results. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of resource operations that may take multiple service requests to iterate over. </returns>
        public static Pageable<ArmResource> ListAtContext(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            return ListAtContextInternal(
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
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <param name="resourceFilters"> Optional filters for results. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of resource operations that may take multiple service requests to iterate over. </returns>
        public static AsyncPageable<ArmResource> ListAtContextAsync(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            return ListAtContextInternalAsync(
                subscription.ClientOptions,
                subscription.Id,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        private static AsyncPageable<ArmResource> ListAtContextInternalAsync(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier scopeId,
            string scopeFilter,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
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

            return ConvertResultsAsync(result, clientOptions);
        }

        private static Pageable<ArmResource> ListAtContextInternal(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier scopeId,
            string scopeFilter = null,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
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

            return ConvertResults(result, clientOptions);
        }

        private static Pageable<ArmResource> ConvertResults(
            Pageable<GenericResourceExpanded> result,
            AzureResourceManagerClientOptions clientOptions)
        {
            return new PhWrappingPageable<GenericResourceExpanded, ArmResource>(
                result,
                CreateResourceConverter(clientOptions));
        }

        private static AsyncPageable<ArmResource> ConvertResultsAsync(
            AsyncPageable<GenericResourceExpanded> result,
            AzureResourceManagerClientOptions clientOptions)
        {
            return new PhWrappingAsyncPageable<GenericResourceExpanded, ArmResource>(
                result,
                CreateResourceConverter(clientOptions));
        }

        private static Func<GenericResourceExpanded, ArmResource> CreateResourceConverter(AzureResourceManagerClientOptions clientOptions)
        {
            return s => Activator.CreateInstance(
                    typeof(ArmResource),
                    clientOptions,
                    Activator.CreateInstance(typeof(ArmResourceData), s as GenericResource) as ArmResourceData) as ArmResource;
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

        private static ResourcesManagementClient GetResourcesClient(AzureResourceManagerClientOptions options, string id)
        {
            return new ResourcesManagementClient(options.BaseUri, id, options.Credential);
        }
    }
}
