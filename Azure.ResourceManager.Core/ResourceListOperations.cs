// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Core.Resources;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Threading;

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
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations"/> instance to use for the list. </param>
        /// <param name="resourceFilters"> Optional filters for results. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of resource operations that may take multiple service requests to iterate over. </returns>
        public static Pageable<ArmResource> ListAtContext(
            ResourceGroupOperations resourceGroup,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            return ListAtContextInternal(
                resourceGroup,
                resourceGroup.Id.Name,
                resourceFilters,
                top,
                cancellationToken);
        }

        /// <summary>
        /// List resources under the a resource context
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations"/> instance to use for the list. </param>
        /// <param name="resourceFilters"> Optional filters for results. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns>An async collection of resource operations that may take multiple service requests to iterate over. </returns>
        public static AsyncPageable<ArmResource> ListAtContextAsync(
            ResourceGroupOperations resourceGroup,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            return ListAtContextInternalAsync(
                resourceGroup,
                resourceGroup.Id.Name,
                resourceFilters,
                top,
                cancellationToken);
        }

        /// <summary>
        /// List resources under a subscription
        /// </summary>
        /// <param name="subscription"> The <see cref="SubscriptionOperations"/> instance to use for the list. </param>
        /// <param name="resourceFilters"> Optional filters for results. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of resource operations that may take multiple service requests to iterate over. </returns>
        public static Pageable<ArmResource> ListAtContext(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            return ListAtContextInternal(
                subscription,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        /// <summary>
        /// List resources under the a resource context
        /// </summary>
        /// <param name="subscription"> The <see cref="SubscriptionOperations"/> instance to use for the list. </param>
        /// <param name="resourceFilters"> Optional filters for results. </param>
        /// <param name="top"> The number of results to return. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of resource operations that may take multiple service requests to iterate over. </returns>
        public static AsyncPageable<ArmResource> ListAtContextAsync(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            return ListAtContextInternalAsync(
                subscription,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        private static ResourcesManagementClient GetResourcesClient(ResourceOperationsBase resourceOperations)
        {
            return new ResourcesManagementClient(resourceOperations.BaseUri, resourceOperations.Id, resourceOperations.Credential);
        }

        private static AsyncPageable<ArmResource> ListAtContextInternalAsync(
            ResourceOperationsBase resourceOperations,
            string scopeFilter,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            var armOperations = GetResourcesClient(resourceOperations).Resources;
            AsyncPageable<GenericResourceExpanded> result;
            if (scopeFilter == null)
            {
                result = armOperations.ListAsync(resourceFilters?.ToString(), null, top, cancellationToken);
            }
            else
            {
                result = armOperations.ListByResourceGroupAsync(
                    scopeFilter,
                    resourceFilters?.ToString(),
                    null,
                    top,
                    cancellationToken);
            }

            return ConvertResultsAsync(result, resourceOperations);
        }

        private static Pageable<ArmResource> ListAtContextInternal(
            ResourceOperationsBase resourceOperations,
            string scopeFilter = null,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            var armOperations = GetResourcesClient(resourceOperations).Resources;
            Pageable<GenericResourceExpanded> result;
            if (scopeFilter == null)
            {
                result = armOperations.List(resourceFilters?.ToString(), null, top, cancellationToken);
            }
            else
            {
                result = armOperations.ListByResourceGroup(
                    scopeFilter,
                    resourceFilters?.ToString(),
                    null,
                    top,
                    cancellationToken);
            }

            return ConvertResults(result, resourceOperations);
        }

        private static Pageable<ArmResource> ConvertResults(
            Pageable<GenericResourceExpanded> result,
            ResourceOperationsBase resourceOperations)
        {
            return new PhWrappingPageable<GenericResourceExpanded, ArmResource>(
                result,
                CreateResourceConverter(resourceOperations));
        }

        private static AsyncPageable<ArmResource> ConvertResultsAsync(
            AsyncPageable<GenericResourceExpanded> result,
            ResourceOperationsBase resourceOperations)
        {
            return new PhWrappingAsyncPageable<GenericResourceExpanded, ArmResource>(
                result,
                CreateResourceConverter(resourceOperations));
        }

        private static Func<GenericResourceExpanded, ArmResource> CreateResourceConverter(ResourceOperationsBase resourceOperations)
        {
            return s => Activator.CreateInstance(
                    typeof(ArmResource),
                    resourceOperations,
                    Activator.CreateInstance(typeof(ArmResourceData), s as GenericResource) as ArmResourceData) as ArmResource;
        }
    }
}
