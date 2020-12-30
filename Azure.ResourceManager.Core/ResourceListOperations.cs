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
        public static Pageable<ArmResourceOperations> ListAtContext<ArmResourceOperations, ArmResource>(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations>
            where ArmResource : TrackedResource
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.AzureResourceType ? id.Name : null;

            return _ListAtContext<ArmResourceOperations, ArmResource>(
                clientOptions,
                id,
                scopeId,
                resourceFilters,
                top,
                cancellationToken);
        }

        public static AsyncPageable<ArmResourceOperations> ListAtContextAsync<ArmResourceOperations, ArmResource>(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier id,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations>
            where ArmResource : TrackedResource
        {
            Validate(id);

            var scopeId = id.Type == ResourceGroupOperations.AzureResourceType ? id.Name : null;

            return _ListAtContextAsync<ArmResourceOperations, ArmResource>(
                clientOptions,
                id,
                scopeId,
                resourceFilters,
                top,
                cancellationToken);
        }

        public static Pageable<ArmResourceOperations> ListAtContext<ArmResourceOperations, ArmResource>(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations>
            where ArmResource : TrackedResource
        {
            return _ListAtContext<ArmResourceOperations, ArmResource>(
                subscription.ClientOptions,
                subscription.Id,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        public static AsyncPageable<ArmResourceOperations> ListAtContextAsync<ArmResourceOperations, ArmResource>(
            SubscriptionOperations subscription,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations>
            where ArmResource : TrackedResource
        {
            return _ListAtContextAsync<ArmResourceOperations, ArmResource>(
                subscription.ClientOptions,
                subscription.Id,
                null,
                resourceFilters,
                top,
                cancellationToken);
        }

        private static AsyncPageable<ArmResourceOperations> _ListAtContextAsync<ArmResourceOperations, ArmResource>(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier scopeId,
            string scopeFilter,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations>
            where ArmResource : TrackedResource
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

            return ConvertResultsAsync<ArmResourceOperations, ArmResource>(result, clientOptions);
        }

        private static Pageable<ArmResourceOperations> _ListAtContext<ArmResourceOperations, ArmResource>(
            AzureResourceManagerClientOptions clientOptions,
            ResourceIdentifier scopeId,
            string scopeFilter = null,
            ArmFilterCollection resourceFilters = null,
            int? top = null,
            CancellationToken cancellationToken = default)
            where ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations>
            where ArmResource : TrackedResource

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

            return ConvertResults<ArmResourceOperations, ArmResource>(result, clientOptions);
        }

        private static Pageable<ArmResourceOperations> ConvertResults<ArmResourceOperations, ArmResource>(
            Pageable<GenericResourceExpanded> result,
            AzureResourceManagerClientOptions clientOptions)
            where ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations>
            where ArmResource : TrackedResource
        {
            return new PhWrappingPageable<GenericResourceExpanded, ArmResourceOperations>(
                result,
                CreateResourceConverter<ArmResourceOperations, ArmResource>(clientOptions));
        }

        private static AsyncPageable<ArmResourceOperations> ConvertResultsAsync<ArmResourceOperations, ArmResource>(
            AsyncPageable<GenericResourceExpanded> result,
            AzureResourceManagerClientOptions clientOptions)
            where ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations>
            where ArmResource : TrackedResource
        {
            return new PhWrappingAsyncPageable<GenericResourceExpanded, ArmResourceOperations>(
                result,
                CreateResourceConverter<ArmResourceOperations, ArmResource>(clientOptions));
        }

        private static Func<GenericResourceExpanded, ArmResourceOperations> CreateResourceConverter<ArmResourceOperations, ArmResource>(AzureResourceManagerClientOptions clientOptions)
            where ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations>
            where ArmResource : TrackedResource
        {
             return s => Activator.CreateInstance(
                    typeof(ArmResourceOperations),
                    clientOptions,
                    Activator.CreateInstance(typeof(ArmResource), s as Azure.ResourceManager.Resources.Models.Resource) as ArmResource) as ArmResourceOperations;
        }

        private static void Validate(ResourceIdentifier id)
        {
            if (id.Type != ResourceGroupOperations.AzureResourceType &&
                id.Type != SubscriptionOperations.AzureResourceType)
            {
                throw new ArgumentException(
                    $"{id.Type} is not valid to list at context must be {ResourceGroupOperations.AzureResourceType} or {SubscriptionOperations.AzureResourceType}");
            }
        }

        //TODO: should be able to access options.GetClient() instead of needing this method
        protected static ResourcesManagementClient GetResourcesClient(AzureResourceManagerClientOptions options, string id)
        {
            return new ResourcesManagementClient(options.BaseUri, id, options.Credential);
        }
    }
}
