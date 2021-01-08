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
        public static Pageable<ArmResource> ListAtContext(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.ResourceType ? id.Name : null;

            return _ListAtContext(
                clientOptions,
                id,
                scopeId,
                resourceFilters,
                top,
                cancellationToken);
        }

        public static AsyncPageable<ArmResource> ListAtContextAsync(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.ResourceType ? id.Name : null;

            return _ListAtContextAsync(
                clientOptions,
                id,
                scopeId,
                resourceFilters,
                top,
                cancellationToken);
        }

        public static Pageable<ArmResource> ListAtContext(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            return _ListAtContext(
                subscription.ClientOptions,
                subscription.Id,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        public static AsyncPageable<ArmResource> ListAtContextAsync(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
        {
            return _ListAtContextAsync(
                subscription.ClientOptions,
                subscription.Id,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        private static AsyncPageable<ArmResource> _ListAtContextAsync(
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

        private static Pageable<ArmResource> _ListAtContext(
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

        protected static ResourcesManagementClient GetResourcesClient(AzureResourceManagerClientOptions options, string id)
        {
            return new ResourcesManagementClient(options.BaseUri, id, options.Credential);
        }
    }
}
