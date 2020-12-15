// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using Azure;
using Azure.ResourceManager.Resources;
using azure_proto_core.Adapters;

namespace azure_proto_core
{
    /// <summary>
    ///     Subscription operations
    /// </summary>
    public class SubscriptionOperations : OperationsBase
    {
        public static readonly ResourceType AzureResourceType = "Microsoft.Resources/subscriptions";

        internal SubscriptionOperations(ArmClientContext context, string defaultSubscription, ArmClientOptions clientOptions)
            : base(context, $"/subscriptions/{defaultSubscription}", clientOptions)
        {
        }

        internal SubscriptionOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : base(context, id, clientOptions)
        {
        }

        internal SubscriptionOperations(ArmClientContext context, Resource subscription, ArmClientOptions clientOptions)
            : base(context, subscription, clientOptions)
        {
        }

        public override ResourceType ResourceType => AzureResourceType;

        internal SubscriptionsOperations SubscriptionsClient => GetClient<ResourcesManagementClient>((uri, cred) =>
            new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred, ArmClientOptions.Convert<ResourcesManagementClientOptions>(ClientOptions))).Subscriptions;

        internal ResourceGroupsOperations RgOperations => GetClient<ResourcesManagementClient>((uri, cred) =>
            new ResourcesManagementClient(uri, Id.Subscription, cred, ArmClientOptions.Convert<ResourcesManagementClientOptions>(ClientOptions))).ResourceGroups;

        public Pageable<ResourceGroupOperations> ListResourceGroups(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroupOperations>(
                RgOperations.List(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new ResourceGroupData(s), ClientOptions));
        }

        public AsyncPageable<ResourceGroupOperations> ListResourceGroupsAsync(
            CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroupOperations>(
                RgOperations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new ResourceGroupData(s), ClientOptions));
        }

        public ResourceGroupOperations ResourceGroup(ResourceGroupData resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, resourceGroup, ClientOptions);
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, resourceGroup, ClientOptions);
        }

        public ResourceGroupOperations ResourceGroup(string resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, $"{Id}/resourceGroups/{resourceGroup}", ClientOptions);
        }

        public ResourceGroupContainer ResourceGroups()
        {
            return new ResourceGroupContainer(ClientContext, this, ClientOptions);
        }
    }
}
